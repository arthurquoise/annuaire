<h1 align="center">
  <br>
  <a href="http://www.amitmerchant.com/electron-markdownify"><img src="https://raw.githubusercontent.com/amitmerchant1990/electron-markdownify/master/app/img/markdownify.png" alt="Markdownify" width="200"></a>
  <br>
  Annuaire
  <br>
</h1>

<h4 align="center">Un projet web en ASP.NET. </h4>

<p align="center">
  <a href="#key-features">Pré-requis</a> •
  <a href="#Installation">Installation</a> •
  <a href="#Fonctionnalités">Fonctionnalités</a> •
  <a href="#credits">Credits</a> •
  <a href="#related">Related</a> •
  <a href="#license">License</a>
</p>

## Pré-requis

* [.NET5.0](<https://dotnet.microsoft.com/en-us/download/dotnet/5.0>)
* MySQL 5.7 ou version supérieure
* PhpMyAdmin ou HeidiSQL
* [Visual Studio](<https://visualstudio.microsoft.com/>)
* [Git](<https://git-scm.com>)

## Installation

Pour cloner et lancer cette application, installer les pré-requis puis :

```
# Ouvrir le dépôt github depuis Visual Studio
$ https://github.com/arthurquoise/annuaire

# Ouvrir annuaire.sql et exécuter la requête dans HeidiSQL ou PHPMyAdmin
$ Attention, utiliser les identifiants suivant pour la connexion SQL :
$ user : "root"
$ password : ""

# Exécuter l'application
$ Compiler le projet en lançant IIs Express

# Panneau d'administration
$ http://localhost:21544/Auth
$ user : admin123
$ password : admin
```

## Fonctionnalités


* Visiteur
	- Recherche multicritères
	- Affichage des informations du collaborateur

* Administration
	- Ajout / Modification / Suppression d’informations 
	- Accès secret au panneau d'administration
	- Redirection en cas d'accès illicite