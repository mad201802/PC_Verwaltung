-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server Version:               10.1.36-MariaDB - mariadb.org binary distribution
-- Server Betriebssystem:        Win32
-- HeidiSQL Version:             9.5.0.5196
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Exportiere Datenbank Struktur für pc_verwaltung
CREATE DATABASE IF NOT EXISTS `pc_verwaltung` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_bin */;
USE `pc_verwaltung`;

-- Exportiere Struktur von Tabelle pc_verwaltung.user
CREATE TABLE IF NOT EXISTS `user` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`username` VARCHAR(64) NOT NULL COLLATE 'utf8_bin',
	`password` TEXT NOT NULL COLLATE 'utf8_bin',
	`email` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_bin',
	`name` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_bin',
	`surname` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_bin',
	`created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	UNIQUE INDEX `username` (`username`),
	UNIQUE INDEX `email` (`email`)
)
COLLATE='utf8_bin'
ENGINE=InnoDB
AUTO_INCREMENT=1
;

-- Exportiere Daten aus Tabelle pc_verwaltung.user: ~1 rows (ungefähr)
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`id`, `username`, `password`, `created_at`, `updated_at`) VALUES
	(NULL, 'admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', NULL, NULL);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
