<?php

declare(strict_types=1);

namespace DoctrineMigrations;

use Doctrine\DBAL\Schema\Schema;
use Doctrine\Migrations\AbstractMigration;

/**
 * Auto-generated Migration: Please modify to your needs!
 */
final class Version20200702141932 extends AbstractMigration
{
    public function getDescription() : string
    {
        return '';
    }

    public function up(Schema $schema) : void
    {
        // this up() migration is auto-generated, please modify it to your needs
        $this->addSql('CREATE TABLE client (id INT AUTO_INCREMENT NOT NULL, achternaam VARCHAR(30) NOT NULL, voornaam VARCHAR(30) NOT NULL, adres VARCHAR(50) NOT NULL, postcode VARCHAR(10) NOT NULL, woonplaats VARCHAR(50) NOT NULL, telefoonnummer VARCHAR(20) DEFAULT NULL, PRIMARY KEY(id)) DEFAULT CHARACTER SET utf8mb4 COLLATE `utf8mb4_unicode_ci` ENGINE = InnoDB');
        $this->addSql('CREATE TABLE gebruiker (id INT AUTO_INCREMENT NOT NULL, gebruikersnaam VARCHAR(180) NOT NULL, roles LONGTEXT NOT NULL COMMENT \'(DC2Type:json)\', password VARCHAR(255) NOT NULL, UNIQUE INDEX UNIQ_89DCDB1FD86EDABB (gebruikersnaam), PRIMARY KEY(id)) DEFAULT CHARACTER SET utf8mb4 COLLATE `utf8mb4_unicode_ci` ENGINE = InnoDB');
        $this->addSql('CREATE TABLE taak (id INT AUTO_INCREMENT NOT NULL, zorgmoment_id INT NOT NULL, omschrijving VARCHAR(30) NOT NULL, voltooid TINYINT(1) DEFAULT NULL, INDEX IDX_2A8AABF6D8CDE8F (zorgmoment_id), PRIMARY KEY(id)) DEFAULT CHARACTER SET utf8mb4 COLLATE `utf8mb4_unicode_ci` ENGINE = InnoDB');
        $this->addSql('CREATE TABLE zorgmoment (id INT AUTO_INCREMENT NOT NULL, gebruiker_id INT NOT NULL, client_id INT NOT NULL, datum_tijd DATETIME NOT NULL, aanwezigheid_begin DATETIME DEFAULT NULL, aanwezigheid_eind DATETIME DEFAULT NULL, opmerkingen VARCHAR(200) DEFAULT NULL, INDEX IDX_A511FED59C92A3DF (gebruiker_id), INDEX IDX_A511FED519EB6921 (client_id), PRIMARY KEY(id)) DEFAULT CHARACTER SET utf8mb4 COLLATE `utf8mb4_unicode_ci` ENGINE = InnoDB');
        $this->addSql('ALTER TABLE taak ADD CONSTRAINT FK_2A8AABF6D8CDE8F FOREIGN KEY (zorgmoment_id) REFERENCES zorgmoment (id)');
        $this->addSql('ALTER TABLE zorgmoment ADD CONSTRAINT FK_A511FED59C92A3DF FOREIGN KEY (gebruiker_id) REFERENCES gebruiker (id)');
        $this->addSql('ALTER TABLE zorgmoment ADD CONSTRAINT FK_A511FED519EB6921 FOREIGN KEY (client_id) REFERENCES client (id)');
    }

    public function down(Schema $schema) : void
    {
        // this down() migration is auto-generated, please modify it to your needs
        $this->addSql('ALTER TABLE zorgmoment DROP FOREIGN KEY FK_A511FED519EB6921');
        $this->addSql('ALTER TABLE zorgmoment DROP FOREIGN KEY FK_A511FED59C92A3DF');
        $this->addSql('ALTER TABLE taak DROP FOREIGN KEY FK_2A8AABF6D8CDE8F');
        $this->addSql('DROP TABLE client');
        $this->addSql('DROP TABLE gebruiker');
        $this->addSql('DROP TABLE taak');
        $this->addSql('DROP TABLE zorgmoment');
    }
}
