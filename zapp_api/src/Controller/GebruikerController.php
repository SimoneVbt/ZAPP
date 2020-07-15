<?php

namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\Routing\Annotation\Route;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;

use App\Service\GebruikerService;


/**
 * @Route("/api/gebruiker")
 */
class GebruikerController extends AbstractController
{
    private $gs;

    public function __construct(GebruikerService $gs)
    {
        $this->gs = $gs;
    }


    /**
     * @Route("/login", name="login")
     */
    public function login(Request $request)
    {
        $params = $request->request->all();
        $result = $this->gs->login($params);

        if ($result) {
            $id = $result[0]["id"];
            return new Response("$id");
        }
        return new Response("-1");
    }
}