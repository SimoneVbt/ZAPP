<?php

namespace App\Entity;

use App\Repository\ZorgmomentRepository;
use Doctrine\ORM\Mapping as ORM;

/**
 * @ORM\Entity(repositoryClass=ZorgmomentRepository::class)
 */
class Zorgmoment
{
    /**
     * @ORM\Id()
     * @ORM\GeneratedValue()
     * @ORM\Column(type="integer")
     */
    private $id;

    /**
     * @ORM\Column(type="integer")
     */
    private $gebruiker_id;

    /**
     * @ORM\Column(type="integer")
     */
    private $client_id;

    /**
     * @ORM\Column(type="datetime")
     */
    private $datum_tijd;

    /**
     * @ORM\Column(type="datetime", nullable=true)
     */
    private $aanwezigheid_begin;

    /**
     * @ORM\Column(type="datetime", nullable=true)
     */
    private $aanwezigheid_eind;

    /**
     * @ORM\Column(type="string", length=200, nullable=true)
     */
    private $opmerkingen;

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getGebruikerId(): ?int
    {
        return $this->gebruiker_id;
    }

    public function setGebruikerId(int $gebruiker_id): self
    {
        $this->gebruiker_id = $gebruiker_id;

        return $this;
    }

    public function getClientId(): ?int
    {
        return $this->client_id;
    }

    public function setClientId(int $client_id): self
    {
        $this->client_id = $client_id;

        return $this;
    }

    public function getDatumTijd(): ?\DateTimeInterface
    {
        return $this->datum_tijd;
    }

    public function setDatumTijd(\DateTimeInterface $datum_tijd): self
    {
        $this->datum_tijd = $datum_tijd;

        return $this;
    }

    public function getAanwezigheidBegin(): ?\DateTimeInterface
    {
        return $this->aanwezigheid_begin;
    }

    public function setAanwezigheidBegin(?\DateTimeInterface $aanwezigheid_begin): self
    {
        $this->aanwezigheid_begin = $aanwezigheid_begin;

        return $this;
    }

    public function getAanwezigheidEind(): ?\DateTimeInterface
    {
        return $this->aanwezigheid_eind;
    }

    public function setAanwezigheidEind(?\DateTimeInterface $aanwezigheid_eind): self
    {
        $this->aanwezigheid_eind = $aanwezigheid_eind;

        return $this;
    }

    public function getOpmerkingen(): ?string
    {
        return $this->opmerkingen;
    }

    public function setOpmerkingen(?string $opmerkingen): self
    {
        $this->opmerkingen = $opmerkingen;

        return $this;
    }
}
