<?php

namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\Routing\Annotation\Route;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;

use App\Service\TaakService;

/**
 * @Route("/api/taak")
 */
class TaakController extends AbstractController
{
    private $ts;

    public function __construct(TaakService $ts)
    {
        $this->ts = $ts;
    }


    /**
     * @Route("/update/{taak_id}", name="update_taak")
     */
    public function updateTaak($taak_id)
    {
        $params = $request->request->all(); //werkt niet in postman
        $params["id"] = $taak_id;

        $result = $this->ts->updateTaak($params);
        if ($result) {
            return new Response("Taakstatus bijgewerkt");
        }
        return new Response("Fout in bijwerken taakstatus");
    }
}