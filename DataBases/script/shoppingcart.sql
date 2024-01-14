CREATE DATABASE  IF NOT EXISTS `shoppingcart` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `shoppingcart`;
-- MySQL dump 10.13  Distrib 8.0.21, for Win64 (x86_64)
--
-- Host: localhost    Database: shoppingcart
-- ------------------------------------------------------
-- Server version	8.0.21

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `account`
--

DROP TABLE IF EXISTS `account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `account` (
  `pk` int NOT NULL AUTO_INCREMENT,
  `id` varchar(50) NOT NULL,
  `pwd` varchar(500) NOT NULL,
  `name` varchar(50) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `role` int DEFAULT '0',
  `date` datetime NOT NULL,
  `status` char(2) NOT NULL DEFAULT 'X',
  PRIMARY KEY (`pk`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account`
--

LOCK TABLES `account` WRITE;
/*!40000 ALTER TABLE `account` DISABLE KEYS */;
INSERT INTO `account` (`pk`, `id`, `pwd`, `name`, `phone`, `role`, `date`, `status`) VALUES (4,'admin','$2a$11$filhVamDJvL3iQo48QwhcuyM.MJ5MLbPqZ25B9ItNULnXBBH9xj/m','admin','0912123123',0,'2024-01-08 09:44:46','V'),(5,'admin1','$2a$11$filhVamDJvL3iQo48QwhcuyM.MJ5MLbPqZ25B9ItNULnXBBH9xj/m','1234','0912123123',0,'2024-01-09 08:51:22','V');
/*!40000 ALTER TABLE `account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `commodity`
--

DROP TABLE IF EXISTS `commodity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commodity` (
  `acc_fk` int NOT NULL,
  `pk` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `pic` varchar(50) DEFAULT NULL,
  `price` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`pk`),
  KEY `acc_fk_idx` (`acc_fk`),
  CONSTRAINT `acc_fk` FOREIGN KEY (`acc_fk`) REFERENCES `account` (`pk`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commodity`
--

LOCK TABLES `commodity` WRITE;
/*!40000 ALTER TABLE `commodity` DISABLE KEYS */;
INSERT INTO `commodity` (`acc_fk`, `pk`, `name`, `pic`, `price`) VALUES (4,1,'CHIMEI 奇美 手持多功能強力氣旋吸塵器(VC-HG0PHA)','40b9330d-bb80-456a-90ec-559bf44a47e1.webp',1388),(4,2,'Suntory 三得利 芝麻明EX 30日份x5瓶(450顆)','6a7bab3c-41e0-4085-b8a8-1dada7e54361.webp',7800);
/*!40000 ALTER TABLE `commodity` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order` (
  `pk` int NOT NULL AUTO_INCREMENT,
  `acc_fk` int NOT NULL,
  `commodity_fk` int NOT NULL,
  `count` int NOT NULL DEFAULT '1',
  `status` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`pk`),
  KEY `accfk_idx` (`acc_fk`),
  KEY `commodityfk_idx` (`commodity_fk`),
  CONSTRAINT `accfk` FOREIGN KEY (`acc_fk`) REFERENCES `account` (`pk`),
  CONSTRAINT `commodityfk` FOREIGN KEY (`commodity_fk`) REFERENCES `commodity` (`pk`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
INSERT INTO `order` (`pk`, `acc_fk`, `commodity_fk`, `count`, `status`) VALUES (8,4,1,5,0);
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-09  9:33:53
