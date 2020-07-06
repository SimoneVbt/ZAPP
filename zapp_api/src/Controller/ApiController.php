<?php

namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\Routing\Annotation\Route;
use Sensio\Bundle\FrameworkExtraBundle\Configuration\Template;
use App\Service\GebruikerService;


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
     * @Route("/{user_id}", name="get_user")
     * @Template()
     */
    public function findUserById($user_id)
    {
        $user = $this->gs->findUserById($user_id);
        $collectie = $user->getZorgmomenten();
        dump($collectie);
        
        die();
        // return $this->json($user);
    }
}
