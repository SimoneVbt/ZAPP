<?php

namespace App\Service;

use App\Entity\Taak;
use Doctrine\ORM\EntityManagerInterface;

class TaakService
{
    private $em;
    private $rep;

    public function __construct(EntityManagerInterface $em)
    {
        $this->em = $em;
        $this->rep = $em->getRepository(Taak::Class);
    }

    public function getTakenByZorgmoment($moment_id)
    {
        return $this->rep->getTakenByZorgmoment($moment_id);
    }
}