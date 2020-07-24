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


    public function checkBeschikbaarheid($params)
    {
        $gebruiker_id = $params["gebruiker_id"];
        $halfuurVroeger = $params["datum_tijd"]->modify("-30 minutes")->format('Y-m-d H:i:s');
        $halfuurLater = $params["datum_tijd"]->modify("+1 hour")->format('Y-m-d H:i:s');

        $beschikbaar = $this->createQueryBuilder("z")
                        ->andWhere("z.gebruiker_id = :gebruiker_id")
                        ->andWhere("z.datum_tijd > :halfuurVroeger")
                        ->andWhere("z.datum_tijd < :halfuurLater")
                        ->setParameter("gebruiker_id", $gebruiker_id)
                        ->setParameter("halfuurVroeger", $halfuurVroeger)
                        ->setParameter("halfuurLater", $halfuurLater)
                        ->getQuery()
                        ->getResult()
                        ;
        return $beschikbaar ? false : true;
    }


    public function getZorgmomentenByGebruiker($user_id, $today, $tomorrow)
    {
        $today = $today->format('Y-m-d');
        $tomorrow = $tomorrow->format('Y-m-d');
        
        $momenten = $this->createQueryBuilder("z")
                    ->where("z.gebruiker_id = $user_id")
                    ->andWhere("z.datum_tijd BETWEEN :today AND :tomorrow")
                    ->setParameter("today", $today)
                    ->setParameter("tomorrow", $tomorrow)
                    ->orderBy("z.datum_tijd", "ASC")
                    ->getQuery()
                    ->getResult()
                    ;
        return $momenten;
    }


    public function createZorgmoment($params)
    {
        if (isset($params["id"])) {
            $moment = $this->find($params["id"]);
        } else {
            $moment = new Zorgmoment();
        }

        $moment->setGebruikerId($params["gebruiker_id"]);
        $moment->setClientId($params["client_id"]);
        $moment->setDatumTijd($params["datum_tijd"]);
        $moment->setOpmerkingen(isset($params["opmerkingen"]) ? $params["opmerkingen"] : null);

        $em = $this->getEntityManager();
        $em->persist($moment);
        $em->flush();

        return $moment;
    }

    
    public function updateZorgmoment($params)
    {
        $moment = $this->find($params["id"]);

        if (isset($params["aanwezigheid_begin"])) {
            $moment->setAanwezigheidBegin( $params["aanwezigheid_begin"]);
        }
        if (isset($params["aanwezigheid_eind"])) {
            $moment->setAanwezigheidEind($params["aanwezigheid_eind"]);
        } 

        $em = $this->getEntityManager();
        $em->persist($moment);
        $em->flush();

        return $moment;
    }


    public function deleteZorgmoment($id)
    {
        $moment = $this->find($id);

        if ($moment) {
            $em = $this->getEntityManager();
            $em->remove($moment);
            $em->flush();
            return true;
        }
        return false;
    }

}
