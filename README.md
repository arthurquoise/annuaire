<h1 align="center">
  <br>
  <a href="https://github.com/arthurquoise/annuaire"><img src="https://s1.qwant.com/thumbr/0x380/0/0/e95c13a46110d51c80904f4085204f21fc79b01a347f510e1bd1b896921925/phone_PNG48991.png?u=https%3A%2F%2Fpngimg.com%2Fuploads%2Fphone%2Fphone_PNG48991.png&q=0&b=1&p=0&a=0" alt="annuaire" width="200"></a>
  <br>
  Annuaire
  <br>
</h1>

<h4 align="center">Un projet web en ASP.NET. </h4>

<p align="center">
  <a href="#key-features">Pr�-requis</a> �
  <a href="#Installation">Installation</a> �
  <a href="#Fonctionnalit�s">Fonctionnalit�s</a> �
</p>

## Pr�-requis

* [.NET5.0](<https://dotnet.microsoft.com/en-us/download/dotnet/5.0>)
* MySQL 5.7 ou version sup�rieure
* PhpMyAdmin ou HeidiSQL
* [Visual Studio](<https://visualstudio.microsoft.com/>)
* [Git](<https://git-scm.com>)

## Installation

Pour cloner et lancer cette application, installer les pr�-requis puis :

```
# Ouvrir le d�p�t github depuis Visual Studio
$ https://github.com/arthurquoise/annuaire

# Ouvrir annuaire.sql et ex�cuter la requ�te dans HeidiSQL ou PHPMyAdmin
$ Attention, utiliser les identifiants suivant pour la connexion SQL :
$ user : "root"
$ password : ""

# Ex�cuter l'application
$ Compiler le projet en lan�ant IIs Express

# Panneau d'administration
$ http://localhost:21544/Auth
$ user : admin123
$ password : admin
```

## Fonctionnalit�s


* Visiteur
	- Recherche multicrit�res
	- Affichage des informations du collaborateur

* Administration
	- Ajout / Modification / Suppression d�informations 
	- Acc�s secret au panneau d'administration
	- Redirection en cas d'acc�s illicite