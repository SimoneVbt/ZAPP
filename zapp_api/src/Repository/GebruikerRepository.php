<?php

namespace App\Repository;

use App\Entity\Gebruiker;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;
use Symfony\Component\Security\Core\Exception\UnsupportedUserException;
use Symfony\Component\Security\Core\User\PasswordUpgraderInterface;
use Symfony\Component\Security\Core\User\UserInterface;


class GebruikerRepository extends ServiceEntityRepository implements PasswordUpgraderInterface
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Gebruiker::class);
    }

    /**
     * Used to upgrade (rehash) the user's password automatically over time.
     */
    public function upgradePassword(UserInterface $user, string $newEncodedPassword): void
    {
        if (!$user instanceof Gebruiker) {
            throw new UnsupportedUserException(sprintf('Instances of "%s" are not supported.', \get_class($user)));
        }

        $user->setPassword($newEncodedPassword);
        $this->_em->persist($user);
        $this->_em->flush();
    }
    

    public function login($params)
    {
        $success = $this->createQueryBuilder("g")
                    ->where("g.gebruikersnaam = :gebruikersnaam")
                    ->andWhere("g.password = :wachtwoord")
                    ->setParameter("gebruikersnaam", $params["gebruikersnaam"])
                    ->setParameter("wachtwoord", $params["wachtwoord"])
                    ->getQuery()
                    ->getResult()
                    ;
        return $success ? true : false;
    }
}
