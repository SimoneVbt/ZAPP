<?php

namespace App\Entity;

use App\Repository\ClientRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\ORM\Mapping as ORM;
use ApiPlatform\Core\Annotation\ApiResource;

/**
 * @ApiResource
 * @ORM\Entity(repositoryClass=ClientRepository::class)
 */
class Client
{
    /**
     * @ORM\Id()
     * @ORM\GeneratedValue()
     * @ORM\Column(type="integer")
     */
    private $id;

    /**
     * @ORM\Column(type="string", length=30)
     */
    private $achternaam;

    /**
     * @ORM\Column(type="string", length=30)
     */
    private $voornaam;

    /**
     * @ORM\Column(type="string", length=50)
     */
    private $adres;

    /**
     * @ORM\Column(type="string", length=10)
     */
    private $postcode;

    /**
     * @ORM\Column(type="string", length=50)
     */
    private $woonplaats;

    /**
     * @ORM\Column(type="string", length=20, nullable=true)
     */
    private $telefoonnummer;

    /**
     * @ORM\OneToMany(targetEntity=Zorgmoment::class, mappedBy="client")
     */
    private $zorgmomenten;

    public function __construct()
    {
        $this->zorgmomenten = new ArrayCollection();
    }

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getAchternaam(): ?string
    {
        return $this->achternaam;
    }

    public function setAchternaam(string $achternaam): self
    {
        $this->achternaam = $achternaam;

        return $this;
    }

    public function getVoornaam(): ?string
    {
        return $this->voornaam;
    }

    public function setVoornaam(string $voornaam): self
    {
        $this->voornaam = $voornaam;

        return $this;
    }

    public function getAdres(): ?string
    {
        return $this->adres;
    }

    public function setAdres(string $adres): self
    {
        $this->adres = $adres;

        return $this;
    }

    public function getPostcode(): ?string
    {
        return $this->postcode;
    }

    public function setPostcode(string $postcode): self
    {
        $this->postcode = $postcode;

        return $this;
    }

    public function getWoonplaats(): ?string
    {
        return $this->woonplaats;
    }

    public function setWoonplaats(string $woonplaats): self
    {
        $this->woonplaats = $woonplaats;

        return $this;
    }

    public function getTelefoonnummer(): ?string
    {
        return $this->telefoonnummer;
    }

    public function setTelefoonnummer(string $telefoonnummer): self
    {
        $this->telefoonnummer = $telefoonnummer;

        return $this;
    }

    /**
     * @return Collection|Zorgmoment[]
     */
    public function getZorgmomenten(): Collection
    {
        return $this->zorgmomenten;
    }

    public function addZorgmomenten(Zorgmoment $zorgmomenten): self
    {
        if (!$this->zorgmomenten->contains($zorgmomenten)) {
            $this->zorgmomenten[] = $zorgmomenten;
            $zorgmomenten->setClient($this);
        }

        return $this;
    }

    public function removeZorgmomenten(Zorgmoment $zorgmomenten): self
    {
        if ($this->zorgmomenten->contains($zorgmomenten)) {
            $this->zorgmomenten->removeElement($zorgmomenten);
            // set the owning side to null (unless already changed)
            if ($zorgmomenten->getClient() === $this) {
                $zorgmomenten->setClient(null);
            }
        }

        return $this;
    }
}
