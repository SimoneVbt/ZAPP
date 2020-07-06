<?php

namespace App\Service;

use Doctrine\ORM\EntityManagerInterface;
use FOS\UserBundle\Model\UserManagerInterface;
use App\Entity\Gebruiker;

class GebruikerService
{
    private $em;
    private $rep;

    public function __construct(EntityManagerInterface $em)
    {
        $this->em = $em;
        $this->rep = $em->getRepository(Gebruiker::class);
    }

    
    public function findUserById($id)
    {
        return $this->rep->findUserById($id);
    }
}