<?php

namespace App\Entity;

use App\Repository\ZorgmomentRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
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
     * @ORM\ManyToOne(targetEntity=Gebruiker::class, inversedBy="zorgmomenten")
     * @ORM\JoinColumn(nullable=false)
     */
    private $gebruiker;

    /**
     * @ORM\ManyToOne(targetEntity=Client::class, inversedBy="zorgmomenten")
     * @ORM\JoinColumn(nullable=false)
     */
    private $client;

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

    /**
     * @ORM\OneToMany(targetEntity=Taak::class, mappedBy="zorgmoment")
     */
    private $taken;

    public function __construct()
    {
        $this->taken = new ArrayCollection();
    }

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getGebruiker(): ?Gebruiker
    {
        return $this->Gebruiker;
    }

    public function setGebruiker(?Gebruiker $gebruiker): self
    {
        $this->gebruiker = $gebruiker;

        return $this;
    }

    public function getClient(): ?Client
    {
        return $this->client;
    }

    public function setClient(?Client $client): self
    {
        $this->client = $client;

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

    /**
     * @return Collection|Taak[]
     */
    public function getTaken(): Collection
    {
        return $this->taken;
    }

    public function addTaken(Taak $taken): self
    {
        if (!$this->taken->contains($taken)) {
            $this->taken[] = $taken;
            $taken->setZorgmoment($this);
        }

        return $this;
    }

    public function removeTaken(Taak $taken): self
    {
        if ($this->taken->contains($taken)) {
            $this->taken->removeElement($taken);
            // set the owning side to null (unless already changed)
            if ($taken->getZorgmoment() === $this) {
                $taken->setZorgmoment(null);
            }
        }

        return $this;
    }
}
