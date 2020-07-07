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

    public function getZorgmomentenByUser($user_id)
    {
        return $this->rep->getZorgmomentenByUser($user_id);
    }
}