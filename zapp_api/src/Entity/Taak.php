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
     * @ORM\ManyToOne(targetEntity=Zorgmoment::class, inversedBy="taken")
     * @ORM\JoinColumn(nullable=false)
     */
    private $zorgmoment;

    /**
     * @ORM\Column(type="string", length=30)
     */
    private $omschrijving;

    /**
     * @ORM\Column(type="boolean", nullable=true)
     */
    private $voltooid;

    /**
     * @ORM\Column(type="integer")
     */
    private $stap;

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getZorgmoment(): ?Zorgmoment
    {
        return $this->zorgmoment;
    }

    public function setZorgmoment(?Zorgmoment $zorgmoment): self
    {
        $this->zorgmoment = $zorgmoment;

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

    public function setVoltooid(?bool $voltooid): self
    {
        $this->voltooid = $voltooid;

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
}
