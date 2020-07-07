<?php

namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\Routing\Annotation\Route;

use App\Service\GebruikerService;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;


/**
 * @Route("/api", name="api")
 */
class ApiController extends AbstractController
{
    private $gs;

    public function __construct(GebruikerService $gs)
    {
        $this->gs = $gs;
    }


    /**
     * @Route("/gebruiker/{user_id}", name="get_user")
     */
    public function findUserById($user_id)
    {
        $user = $this->gs->findUserById($user_id);
        $momenten = $user->getZorgmomenten();

        foreach ($momenten as $moment) {
            $result[] = $moment;
        }
        
        // foreach ($momenten as $moment) {
        //     $client = $moment->getClient()->getId();

        //     $taken = $moment->getTaken();
        //     foreach ($taken as $taak) {
        //         $taak_id = $taak->getId();
        //     }
        //     // $result = $c->getDatumTijd();
        //     // $array[] = $result;
        // }
        // dump($user);
        // die();
        
        return $this->json($result);
    }    
}
