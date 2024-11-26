Sujet : Création d’un Pokemon-Like en C# avec WPF 
Votre devez développer une application Pokemon-Like(basé  sur le 
combat) en C# utilisant Windows Presentation Foundation (WPF). Cette 
application doit intégrer une interface utilisateur ergonomique, la gestion 
d'une base de données SQL server express pour stocker les données du jeu 
(monstres, spell, etc.), et des fonctionnalités de combat. 
Consignes & contrainte 
• Le modèle de la BDD ne doit pas changer 
• Vous avez le droit de crée vos propres sets de donnée si vous le 
souhaitez mais pas de changer le modèle de la BDD (ajouter 
colonne, nouvelle table …) 
• Chaque monstre possède 4 spells. 
Un joueur possède un monstre. 
Le combat est un tour par tour 
• A noté que si la connexion à la base de données ne fonctionne 
pas, tout ce qui est lié à la base de données sera compte comme 
ne fonctionne pas donc 0. 
• L’URL est de connexion doit être un champ a renseigné dans 
l’onglet setting 
• Les indications du projet doivent être présenté dans un readme 
(Initialisation jeu de donnée, package utiliser, explications des 
features…) 
• Le rendu doit être sur github, assurez vous d’avoir votre répo en 
public 
Le non-respect des contrainte peut entrainer des pénalités 
Le barème peut être amener a évolué 
Objectifs du projet - - - 
POO (Pensez a séparé les responsabilités !) – 3 points 
Séparation des ressources – 2 points 
Respect du modèle MVVM – 3 points 
1. Écran de connexion (Login) – 4 points 
o Permettre aux utilisateurs de se connecter avec un nom d’utilisateur et un mot 
de passe (le mot de passe doit être hash en BASE). (2 points) 
o Les informations de connexion seront stockées dans la base de données. 
(2 points) 
2. Ecran setting de la base de données – 2 points 
• Initialiser la base de données avec un jeu de données par défaut (monstres, 
sorts, utilisateurs, etc.). (2 points) 
3. Fenêtre de gestion des monstres - 6 points 
o Une fenêtre listant tous les monstres disponibles avec la possibilité de choisir un 
monstre à jouer (3 points) 
o Un affichage détaillé des informations du monstre sélectionné (Name, HP, spell 
associés, etc.). (3 points) 
4. Onglet des Spells - 5 Points 
o Liste des spells existants dans le jeu. (1 points) 
o Un affichage détaillé de chaque spell : nom, dégâts, description. (3 points) 
o Un système de tri selon le monstre qui le possède (1 point) 
5. Onglet Combat - (10 points) 
Simuler un combat entre un monstre joueur et un monstre ennemi. 
o Le combat doit inclure : 
▪ L’utilisation des spells pour infliger des dégâts (4 points) 
▪ Une barre d’hp visible pour chaque monstre. (2 points) 
▪ Générer un nouveau monstre ennemi avec des statistiques légèrement 
améliorées (par exemple, +10% d’hp ou +5% de dégâts de plus sur ses 
spells). (2 points) 
o Afficher un bouton pour relancer un combat avec un nouveau monstre. (1 points) 
o Crée un score à chaque monstre vaincu. (1 points) 
Script de la création de la BDD :  -- Création de la base de données 
CREATE DATABASE ExerciceMonster; 
GO -- Utilisation de la base de données 
USE ExerciceMonster; 
GO -- Table Login 
CREATE TABLE Login ( 
ID INT PRIMARY KEY IDENTITY(1,1), 
Username NVARCHAR(50) NOT NULL, 
PasswordHash NVARCHAR(255) NOT NULL 
); -- Table Player 
CREATE TABLE Player ( 
ID INT PRIMARY KEY IDENTITY(1,1), 
Name NVARCHAR(50) NOT NULL, 
LoginID INT, 
FOREIGN KEY (LoginID) REFERENCES Login(ID) 
); -- Table Monster 
CREATE TABLE Monster ( 
ID INT PRIMARY KEY IDENTITY(1,1), 
Name NVARCHAR(50) NOT NULL, 
Health INT NOT NULL 
); -- Table Spell 
CREATE TABLE Spell ( 
ID INT PRIMARY KEY IDENTITY(1,1), 
Name NVARCHAR(50) NOT NULL, 
Damage INT NOT NULL, 
Description NVARCHAR(MAX) 
); -- Table PlayerMonster (relation Player <-> Monster) 
CREATE TABLE PlayerMonster ( 
PlayerID INT NOT NULL, 
MonsterID INT NOT NULL, 
PRIMARY KEY (PlayerID, MonsterID), 
FOREIGN KEY (PlayerID) REFERENCES Player(ID), 
FOREIGN KEY (MonsterID) REFERENCES Monster(ID) 
); -- Table MonsterSpell (relation Monster <-> Spell) 
CREATE TABLE MonsterSpell ( 
MonsterID INT NOT NULL, 
SpellID INT NOT NULL, 
PRIMARY KEY (MonsterID, SpellID), 
FOREIGN KEY (MonsterID) REFERENCES Monster(ID), 
FOREIGN KEY (SpellID) REFERENCES Spell(ID) 
); 