-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 23, 2019 at 12:52 AM
-- Server version: 10.1.30-MariaDB
-- PHP Version: 7.2.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `edeja_base`
--

-- --------------------------------------------------------

--
-- Table structure for table `fakture`
--

CREATE TABLE `fakture` (
  `xID` int(11) NOT NULL,
  `xBroj` varchar(10) NOT NULL,
  `xDatum` date NOT NULL,
  `xUkupno` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `korisnici`
--

CREATE TABLE `korisnici` (
  `xID` int(11) NOT NULL,
  `xUser` varchar(255) NOT NULL,
  `xPassword` varchar(255) NOT NULL,
  `xStatus` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `korisnici`
--

INSERT INTO `korisnici` (`xID`, `xUser`, `xPassword`, `xStatus`) VALUES
(4, 'Milan', '81dc9bdb52d04dc20036dbd8313ed055', 2);

-- --------------------------------------------------------

--
-- Table structure for table `stavke_faktura`
--

CREATE TABLE `stavke_faktura` (
  `xID` int(11) NOT NULL,
  `xRedniBroj` int(11) NOT NULL,
  `xKolicina` int(11) NOT NULL,
  `xCena` int(11) NOT NULL,
  `xUkupno` int(11) NOT NULL,
  `xBrojFakture` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `fakture`
--
ALTER TABLE `fakture`
  ADD PRIMARY KEY (`xID`),
  ADD UNIQUE KEY `xBroj` (`xBroj`);

--
-- Indexes for table `korisnici`
--
ALTER TABLE `korisnici`
  ADD PRIMARY KEY (`xID`);

--
-- Indexes for table `stavke_faktura`
--
ALTER TABLE `stavke_faktura`
  ADD PRIMARY KEY (`xID`),
  ADD UNIQUE KEY `xRedniBroj` (`xRedniBroj`),
  ADD KEY `xBrojFakture` (`xBrojFakture`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `fakture`
--
ALTER TABLE `fakture`
  MODIFY `xID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `korisnici`
--
ALTER TABLE `korisnici`
  MODIFY `xID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `stavke_faktura`
--
ALTER TABLE `stavke_faktura`
  MODIFY `xID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `stavke_faktura`
--
ALTER TABLE `stavke_faktura`
  ADD CONSTRAINT `stavke_faktura_ibfk_1` FOREIGN KEY (`xBrojFakture`) REFERENCES `fakture` (`xID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
