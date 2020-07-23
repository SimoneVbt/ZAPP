<?php

namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\Routing\Annotation\Route;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;

use App\Service\ZorgmomentService;
use App\Service\TaakService;
use App\Service\ClientService;


/**
 * @Route("/api/zorgmoment")
 */
class ZorgmomentController extends AbstractController
{
    private $zs;
    private $ts;
    private $cs;

    public function __construct(ZorgmomentService $zs,
                                TaakService $ts,
                                ClientService $cs)
    {
        $this->zs = $zs;
        $this->ts = $ts;
        $this->cs = $cs;
    }


    /**
     * @Route("/delete/{moment_id}", name="delete_zorgmoment")
     */
    public function deleteZorgmoment($moment_id)
    {
        $result = $this->zs->deleteZorgmoment($moment_id);
        if ($result) {
            return new Response("Zorgmoment verwijderd");
        }
        return new Response("Fout bij verwijderen zorgmoment");
    }

    
    /**
     * @Route("/update/{moment_id}", name="update_zorgmoment")
     */
    public function updateZorgmoment(Request $request, $moment_id)
    {
        $params = $request->request->all();
        $params["id"] = $moment_id;

        $result = $this->zs->updateZorgmoment($params);
        if ($result) {
            return new Response ("Aanwezigheid bijgewerkt");
        }
        return new Response("Fout in bijwerken aanwezigheid");
    }


    /**
     * @Route("/create", name="create_zorgmoment")
     */
    public function createZorgmoment(Request $request)
    {        
        $params = $request->request->all();

        $available = $this->zs->checkBeschikbaarheid($params);
        if ($available) {

            $result = $this->zs->createZorgmoment($params);
            if ($result) {
                return new Response("Zorgmoment gecreëerd");
            }
            return new Response("Fout in creatie zorgmoment");
        } 
        return new Response("De zorgverlener is niet beschikbaar op het gekozen tijdstip.");
    }

     /**
     * @Route("/get/{user_id}",
     *  name="get_zorgmomenten",
     *  requirements={"page"="\d+"})
     */
    public function getZorgmomentenByGebruiker($user_id)
    {
        $momenten = $this->zs->getZorgmomentenByGebruiker($user_id);
        $data = [];

        foreach ($momenten as $moment)
        {
            $moment_id = $moment->getId();
            $taken = $this->ts->getTakenByZorgmoment($moment_id);
            
            $client_id = $moment->getClientId();
            $client = $this->cs->findClient($client_id);

            $data[] = [
                "moment" => $moment,
                "taken" => $taken,
                "client" => $client
            ];
        }

        $json = $this->renderView('api.json.twig', ["data" => $data]);
        $response = new Response($json);
        $response->headers->set('Content-Type','application/json');
        return $response;
    }



}
