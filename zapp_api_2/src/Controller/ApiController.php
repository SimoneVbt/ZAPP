<?php

namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\Routing\Annotation\Route;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;

use App\Service\ZorgmomentService;
use App\Service\TaakService;


/**
 * @Route("/api", name="api")
 */
class ApiController extends AbstractController
{
    private $zs;

    public function __construct(ZorgmomentService $zs,
                                TaakService $ts)
    {
        $this->zs = $zs;
        $this->ts = $ts;
    }

     /**
     * @Route("/zorgmomenten/{user_id}", name="get_zorgmomenten")
     */
    public function getZorgmomentenByUser($user_id)
    {
        $momenten = $this->zs->getZorgmomentenByUser($user_id);

        foreach ($momenten as $moment)
        {
            $moment_id = $moment->getId();
            $taken = $this->ts->getTakenByZorgmoment($moment_id);
            $moment->{"taken"} = $taken; //werkt nog niet
        }

        return $this->json($momenten);
    }

    /**
     * @Route("/taken/{moment_id}", name="get_taken")
     */
    public function getTakenByZorgmoment($moment_id)
    {
        $taken = $this->ts->getTakenByZorgmoment($moment_id);
        return $this->json($taken);
    }
}
