<?php

namespace App\Entity;

use App\Repository\GebruikerRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\ORM\Mapping as ORM;
use Symfony\Component\Security\Core\User\UserInterface;

/**
 * @ORM\Entity(repositoryClass=GebruikerRepository::class)
 */
class Gebruiker implements UserInterface
{
    /**
     * @ORM\Id()
     * @ORM\GeneratedValue()
     * @ORM\Column(type="integer")
     */
    private $id;

    /**
     * @ORM\Column(type="string", length=180, unique=true)
     */
    private $gebruikersnaam;

    /**
     * @ORM\Column(type="json")
     */
    private $roles = [];

    /**
     * @var string The hashed password
     * @ORM\Column(type="string")
     */
    private $password;

    /**
     * @ORM\OneToMany(targetEntity=Zorgmoment::class, mappedBy="Gebruiker")
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

    public function getGebruikersnaam(): ?string
    {
        return $this->gebruikersnaam;
    }

    public function setGebruikersnaam(string $gebruikersnaam): self
    {
        $this->gebruikersnaam = $gebruikersnaam;

        return $this;
    }

    /**
     * A visual identifier that represents this user.
     *
     * @see UserInterface
     */
    public function getUsername(): string
    {
        return (string) $this->gebruikersnaam;
    }

    /**
     * @see UserInterface
     */
    public function getRoles(): array
    {
        $roles = $this->roles;
        // guarantee every user at least has ROLE_USER
        $roles[] = 'ROLE_USER';

        return array_unique($roles);
    }

    public function setRoles(array $roles): self
    {
        $this->roles = $roles;

        return $this;
    }

    /**
     * @see UserInterface
     */
    public function getPassword(): string
    {
        return (string) $this->password;
    }

    public function setPassword(string $password): self
    {
        $this->password = $password;

        return $this;
    }

    /**
     * @see UserInterface
     */
    public function getSalt()
    {
        // not needed when using the "bcrypt" algorithm in security.yaml
    }

    /**
     * @see UserInterface
     */
    public function eraseCredentials()
    {
        // If you store any temporary, sensitive data on the user, clear it here
        // $this->plainPassword = null;
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
            $zorgmomenten->setGebruiker($this);
        }

        return $this;
    }

    public function removeZorgmomenten(Zorgmoment $zorgmomenten): self
    {
        if ($this->zorgmomenten->contains($zorgmomenten)) {
            $this->zorgmomenten->removeElement($zorgmomenten);
            // set the owning side to null (unless already changed)
            if ($zorgmomenten->getGebruiker() === $this) {
                $zorgmomenten->setGebruiker(null);
            }
        }

        return $this;
    }
}
