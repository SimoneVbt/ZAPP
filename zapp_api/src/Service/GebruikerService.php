<?php

namespace App\Service;

use App\Entity\Gebruiker;
use Doctrine\ORM\EntityManagerInterface;

class GebruikerService
{
    private $em;
    private $rep;

    public function __construct(EntityManagerInterface $em)
    {
        $this->em = $em;
        $this->rep = $em->getRepository(Gebruiker::class);
    }


    public function login($params)
    {
        return $this->rep->login($params);
    }
}