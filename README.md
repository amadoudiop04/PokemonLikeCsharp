# Project PokemonLike
**Welcome to PokemonLike**

<img src="./Ressources\images\PokemonLikeProject1.png" with="800" />
<img src="./Ressources\images\PokemonLikeProject.png" with="800" />

the aim of this project is to develop a combat-based wpf application in c# using Windows Presentation Foundation (wpf), integrating an ergonomic user interface, sql server express database management to store game data and combat features.

# Prerequisite⏪
Somes knowledges in this field :

- **The use of C#**

- **The use of SQL management, SQL(
Structured Query Language), WPF and Microsoft Visual Studios**

- **The use of Git and for the code management**

# Installation🔧

### 1.Clone the repository :

git clone https://github.com/amadoudiop04/PokemonLikeCsharp.git

# Start 🧑‍💻

Once the project has been cloned via Microsoft Visual Studio, you'll see a green button at the top of the page. 
Click on it to launch the program.

-Once the project has been launched, make sure that the URL to your database is in the following format:

Data source=localhost\SQLEXPRESS;Database=ExerciceMonster;Trusted_Connection=True;TrustServerCertificate=True;

In particular, you can use the admin interface to log in with the following credentials or create an account:

-Username: admin

-Password: admin123

# How Play ? 🔨

Once you've logged in, you'll be taken to a page displaying a list of monsters. At the bottom right of this page, you'll find two buttons: Spell and Combat.

Spell: This button displays the list of monster spells in greater detail.

Combat: This button transports you to an arena where your monster will face off against an enemy monster.

Once you've selected your monster, you can simulate the fight by pressing the flame icon in the center of the page until the fight is over.

# Version 🗃️
-NET 9

-SQL server management V20.2

NB: si vous ne disposez pas de Base de données SQL server vous pouvais le creer en executant se script :
  -- Cr�ation de la base de donn�es 
CREATE DATABASE ExerciceMonster; 
GO 
-- Utilisation de la base de donn�es 
USE ExerciceMonster; 
GO 

-- Table Login 
CREATE TABLE Login ( 
ID INT PRIMARY KEY IDENTITY(1,1), 
Username NVARCHAR(50) NOT NULL, 
PasswordHash NVARCHAR(255) NOT NULL 
); 

-- Table Player 
CREATE TABLE Player ( 
ID INT PRIMARY KEY IDENTITY(1,1), 
Name NVARCHAR(50) NOT NULL, 
LoginID INT, 
FOREIGN KEY (LoginID) REFERENCES Login(ID) 
); 

-- Table Monster 
CREATE TABLE Monster ( 
ID INT PRIMARY KEY IDENTITY(1,1), 
Name NVARCHAR(50) NOT NULL, 
Health INT NOT NULL 
); 

-- Table Spell 
CREATE TABLE Spell ( 
ID INT PRIMARY KEY IDENTITY(1,1), 
Name NVARCHAR(50) NOT NULL, 
Damage INT NOT NULL, 
Description NVARCHAR(MAX) 
); 

-- Table PlayerMonster (relation Player <-> Monster) 
CREATE TABLE PlayerMonster ( 
PlayerID INT NOT NULL, 
MonsterID INT NOT NULL, 
PRIMARY KEY (PlayerID, MonsterID), 
FOREIGN KEY (PlayerID) REFERENCES Player(ID), 
FOREIGN KEY (MonsterID) REFERENCES Monster(ID) 
); 

-- Table MonsterSpell (relation Monster <-> Spell)
CREATE TABLE MonsterSpell ( 
MonsterID INT NOT NULL, 
SpellID INT NOT NULL, 
PRIMARY KEY (MonsterID, SpellID), 
FOREIGN KEY (MonsterID) REFERENCES Monster(ID), 
FOREIGN KEY (SpellID) REFERENCES Spell(ID) 
);

-Puis inserer via des requetes SQL les données suivants :

-1.

INSERT INTO [ExerciceMonster].[dbo].[Monster] ([ID], [Name], [Health]) 
VALUES 
(1, 'Charmander', 120),
(2, 'Squirtle', 110),
(3, 'Bulbasaur', 130),
(4, 'Jigglypuff', 90),
(5, 'Meowth', 100),
(6, 'Pikachu', 100),
(7, 'Psyduck', 105),
(8, 'Machop', 140),
(9, 'Snorlax', 200),
(10, 'Gengar', 150);

-2.

INSERT INTO [ExerciceMonster].[dbo].[Spell] ([ID], [Name], [Damage], [Description]) 
VALUES 
(1, 'Flamethrower', 60, 'Fire attack'),
(2, 'Water Gun', 50, 'Water attack'),
(3, 'Vine Whip', 55, 'Grass attack'),
(4, 'Sing', 0, 'Puts the enemy to sleep'),
(5, 'Scratch', 40, 'Normal attack'),
(6, 'Thunderbolt', 50, 'Electric attack'),
(7, 'Confusion', 65, 'Psychic attack'),
(8, 'Karate Chop', 70, 'Fighting attack'),
(9, 'Body Slam', 80, 'Normal attack'),
(10, 'Shadow Ball', 75, 'Ghost attack');



# Auteurs 💸

- [Amadou diop ⚽💻] (linkedin : https://www.linkedin.com/in/amadou-diop-3a5258316/?trk=opento_sprofile_details)