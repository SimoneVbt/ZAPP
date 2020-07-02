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

    // /**
    //  * @return Zorgmoment[] Returns an array of Zorgmoment objects
    //  */
    /*
    public function findByExampleField($value)
    {
        return $this->createQueryBuilder('z')
            ->andWhere('z.exampleField = :val')
            ->setParameter('val', $value)
            ->orderBy('z.id', 'ASC')
            ->setMaxResults(10)
            ->getQuery()
            ->getResult()
        ;
    }
    */

    /*
    public function findOneBySomeField($value): ?Zorgmoment
    {
        return $this->createQueryBuilder('z')
            ->andWhere('z.exampleField = :val')
            ->setParameter('val', $value)
            ->getQuery()
            ->getOneOrNullResult()
        ;
    }
    */
}
