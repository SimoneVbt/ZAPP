<?php

namespace App\Entity;

use App\Repository\TaakRepository;
use Doctrine\ORM\Mapping as ORM;

/**
 * @ORM\Entity(repositoryClass=TaakRepository::class)
 */
class Taak
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
    private $zorgmoment_id;

    /**
     * @ORM\Column(type="integer")
     */
    private $stap;

    /**
     * @ORM\Column(type="string", length=30)
     */
    private $omschrijving;

    /**
     * @ORM\Column(type="boolean", nullable=true)
     */
    private $voltooid;

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getZorgmomentId(): ?int
    {
        return $this->zorgmoment_id;
    }

    public function setZorgmomentId(int $zorgmoment_id): self
    {
        $this->zorgmoment_id = $zorgmoment_id;

        return $this;
    }

    public function getStap(): ?int
    {
        return $this->stap;
    }

    public function setStap(int $stap): self
    {
        $this->stap = $stap;

        return $this;
    }

    public function getOmschrijving(): ?string
    {
        return $this->omschrijving;
    }

    public function setOmschrijving(string $omschrijving): self
    {
        $this->omschrijving = $omschrijving;

        return $this;
    }

    public function getVoltooid(): ?bool
    {
        return $this->voltooid;
    }

    public function setVoltooid(bool $voltooid): self
    {
        $this->voltooid = $voltooid;

        return $this;
    }
}
