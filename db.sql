/*
SQLyog Ultimate v12.09 (64 bit)
MySQL - 5.7.12-log : Database - teamfightdb
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`teamfightdb` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `teamfightdb`;

/*Table structure for table `friends` */

DROP TABLE IF EXISTS `friends`;

CREATE TABLE `friends` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CharacterId` int(11) NOT NULL,
  `FriendCharacterId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_CharacterId_Characters_Id` (`CharacterId`),
  KEY `FK_FriendCharacterId_Characters_Id` (`FriendCharacterId`),
  CONSTRAINT `FK_CharacterId_Characters_Id` FOREIGN KEY (`CharacterId`) REFERENCES `players` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_FriendCharacterId_Characters_Id` FOREIGN KEY (`FriendCharacterId`) REFERENCES `players` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

/*Data for the table `friends` */

insert  into `friends`(`Id`,`CharacterId`,`FriendCharacterId`) values (1,1,2),(2,1,3),(3,1,4),(4,4,1),(5,2,1),(6,1,5),(7,5,1);

/*Table structure for table `players` */

DROP TABLE IF EXISTS `players`;

CREATE TABLE `players` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(6) NOT NULL,
  `Gender` tinyint(1) NOT NULL,
  `Level` int(10) unsigned NOT NULL,
  `PhysicalStrength` int(10) unsigned NOT NULL,
  `Endurance` int(10) unsigned NOT NULL,
  `CE` int(10) unsigned NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Data for the table `players` */

insert  into `players`(`Id`,`Name`,`Gender`,`Level`,`PhysicalStrength`,`Endurance`,`CE`) values (1,'玩家A',1,22,150,0,0),(2,'玩家B',0,13,180,0,0),(3,'玩家C',1,25,130,0,0),(4,'玩家D',1,38,112,0,0),(5,'玩家E',0,46,144,0,0),(6,'玩家F',0,47,178,0,0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
