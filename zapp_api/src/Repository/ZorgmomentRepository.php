<?php

namespace App\Repository;

use App\Entity\Zorgmoment;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

/**
 * @method Zorgmoment|null find($id, $lockMode = null, $lockVersion = null)
 * @method Zorgmoment|null findOneBy(array $criteria, array $orderBy = null)
 * @method Zorgmoment[]    findAll()
 * @method Zorgmoment[]    findBy(array $criteria, array $orderBy = null, $limit = null, $offset = null)
 */
class ZorgmomentRepository extends ServiceEntityRepository
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Zorgmoment::class);
    }

    // public function getAllZorgmomentenByUserId($user_id)
    // {
    //     $zorgmomenten = $this->createQueryBuilder("z")
    //                     ->where("z.gebruiker_id = $user_id")
    //                     ->orderBy("z.datum_tijd", "DESC")
    //                     ->setMaxResults(20)
    //                     ->getQuery()
    //                     ->getResult();
    //     return $zorgmomenten;
    // }
}
