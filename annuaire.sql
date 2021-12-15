-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : mer. 15 déc. 2021 à 11:47
-- Version du serveur :  10.4.21-MariaDB
-- Version de PHP : 8.0.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+01:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `annuaire`
--

-- --------------------------------------------------------

--
-- Structure de la table `department`
--

CREATE TABLE `department` (
  `department_id` int(11) NOT NULL,
  `department_name` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `department`
--

INSERT INTO `department` (`department_id`, `department_name`) VALUES
(1, 'informatique'),
(2, 'RH'),
(3, 'Comptabilité'),
(4, 'Production'),
(5, 'Commercial'),
(6, 'Accueil');

--
-- Structure de la table `site`
--

CREATE TABLE `site` (
  `site_id` int(11) NOT NULL,
  `site_name` varchar(50) NOT NULL,
  `site_type` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `site`
--

INSERT INTO `site` (`site_id`, `site_name`, `site_type`) VALUES
(1, 'Nice', 'Production'),
(2, 'Paris', 'Siège Administratif'),
(3, 'Nantes', 'Production'),
(4, 'Toulouse', 'Production'),
(5, 'Lille', 'Production');

-- --------------------------------------------------------

--
-- Structure de la table `user_login`
--

CREATE TABLE `user_login` (
  `user_id` int(11) NOT NULL,
  `user_name` varchar(50) DEFAULT NULL,
  `user_password` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `user_login`
--

INSERT INTO `user_login` (`user_id`, `user_name`, `user_password`) VALUES
(1, 'admin123', 'admin');

-- --------------------------------------------------------

--
-- Structure de la table `employee`
--

CREATE TABLE `employee` (
  `employee_id` int(11) NOT NULL,
  `firstname` varchar(50) NOT NULL,
  `lastname` varchar(50) NOT NULL,
  `landline_phone` varchar(10) DEFAULT NULL,
  `mobile_phone` varchar(10) DEFAULT NULL,
  `e_mail` varchar(50) DEFAULT NULL,
  `site_id` int(11) NOT NULL,
  `department_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `employee`
--

INSERT INTO `employee` (`employee_id`, `firstname`, `lastname`, `landline_phone`, `mobile_phone`, `e_mail`, `site_id`, `department_id`) VALUES
(1, 'Arthur', 'DENNETIERE', '0103405060', '0602304050', 'arthur.dennetiere@annu.fr', 5, 1),
(2, 'Nicolas', 'HULOT', '0103405061', '0602304051', 'nicolas.hulot@annu.fr', 3, 5),
(3, 'Martin', 'BURRIEZ', '0103405062', '0602304052', 'martin.burriez@annu.fr', 1, 6),
(4, 'Mathilde', 'RENARD', '0103405063', '0602304053', 'mathilde.renard@annu.fr', 2, 4),
(5, 'Valentin', 'VERRIN', '0103405064', '0602304054', 'valentin.verrin@annu.fr', 4, 2),
(6, 'Steven', 'CARRON', '0103405065', '0602304055', 'steven.carron@annu.fr', 2, 3),
(7, 'Maxence', 'VANDERK', '0103405038', '0603455038', 'maxence.vanderk@annu.fr', 3, 5);

-- --------------------------------------------------------

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `department`
--
ALTER TABLE `department`
  ADD PRIMARY KEY (`department_id`);

--
-- Index pour la table `employee`
--
ALTER TABLE `employee`
  ADD PRIMARY KEY (`employee_id`),
  ADD KEY `site_id` (`site_id`),
  ADD KEY `department_id` (`department_id`);

--
-- Index pour la table `site`
--
ALTER TABLE `site`
  ADD PRIMARY KEY (`site_id`);

--
-- Index pour la table `user_login`
--
ALTER TABLE `user_login`
  ADD PRIMARY KEY (`user_id`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `department`
--
ALTER TABLE `department`
  MODIFY `department_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT pour la table `employee`
--
ALTER TABLE `employee`
  MODIFY `employee_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT pour la table `site`
--
ALTER TABLE `site`
  MODIFY `site_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT pour la table `user_login`
--
ALTER TABLE `user_login`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `employee`
--
ALTER TABLE `employee`
  ADD CONSTRAINT `employee_ibfk_1` FOREIGN KEY (`site_id`) REFERENCES `site` (`site_id`),
  ADD CONSTRAINT `employee_ibfk_2` FOREIGN KEY (`department_id`) REFERENCES `department` (`department_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
