<?php

namespace App\Repository;

use App\Entity\Taak;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

class TaakRepository extends ServiceEntityRepository
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Taak::class);
    }

    public function getTakenByZorgmoment($moment_id)
    {
        $taken = $this->createQueryBuilder("t")
                ->where("t.zorgmoment_id = $moment_id")
                ->orderBy("t.stap", "ASC")
                ->getQuery()
                ->getResult()
                ;
        return $taken;
    }
}
