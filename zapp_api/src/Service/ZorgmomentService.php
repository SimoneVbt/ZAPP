<?php

namespace App\Service;

use App\Entity\Zorgmoment;
use Doctrine\ORM\EntityManagerInterface;

class ZorgmomentService
{
    private $em;
    private $rep;

    public function __construct(EntityManagerInterface $em)
    {
        $this->em = $em;
        $this->rep = $em->getRepository(Zorgmoment::Class);
    }


    public function checkBeschikbaarheid($params)
    {
        $params["datum_ijd"] = new \DateTime($params["datum_tijd"]);
        return $this->rep->checkBeschikbaarheid($params);    
    }


    public function getZorgmomentenByGebruiker($user_id)
    {
        $today = new \DateTime();
        $tomorrow = new \DateTime();
        $tomorrow->modify("+2 days");

        return $this->rep->getZorgmomentenByGebruiker($user_id, $today, $tomorrow);
    }


    public function createZorgmoment($params)
    {
        $params["datum_tijd"] = new \DateTime($params["datum_tijd"]);
        return $this->rep->createZorgmoment($params);
    }


    public function updateZorgmoment($params)
    {
        if (isset($params["aanwezigheid_begin"])) {
            $params["aanwezigheid_begin"] = new \DateTime($params["aanwezigheid_begin"]);
        }
        if (isset($params["aanwezigheid_eind"])) {
            $params["aanwezigheid_eind"] = new \DateTime($params["aanwezigheid_eind"]);
        }
        return $this->rep->updateZorgmoment($params);
    }


    public function deleteZorgmoment($id)
    {
        return $this->rep->deleteZorgmoment($id);
    }
}