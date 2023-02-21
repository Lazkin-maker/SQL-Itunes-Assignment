CREATE DATABASE SuperheroesDb;



use SuperheroesDb;

CREATE TABLE Superhero(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
	Alias VARCHAR(50) NOT NULL,
	Origin VARCHAR(50) NOT NULL,
);

CREATE TABLE Assistant(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(50) NOT NULL
);

CREATE TABLE Power(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
	Description VARCHAR(100) NOT NULL
);




ALTER TABLE Assistant
ADD SuperheroId INT NULL,
CONSTRAINT FK_Assistant_Superhero FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id);


CREATE TABLE SuperheroPower(
	SuperheroId INT NOT NULL,
	PowerId INT NOT NULL,

	CONSTRAINT PK_SuperheroPower PRIMARY KEY (SuperheroId, PowerId),
	CONSTRAINT FK_SuperheroPower_Superhero FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id),
	CONSTRAINT FK_SuperheroPower_Power FOREIGN KEY (PowerId) REFERENCES Power(Id)
);



INSERT INTO Superhero(Name, Alias, Origin) VALUES('Hero1', 'Alias1', 'Origin1'),
												('Hero2', 'Alias2', 'Origin2'),
												('Hero3', 'Alias3', 'Origin3');




INSERT INTO Assistant(Name, SuperheroId) VALUES('Assistant1', 1),
												('Assistant2', 2),
												('Assistant3', 3);






INSERT INTO Power(Name, Description) VALUES('Flight', 'Ability to fly' ),
											('Super Strength', 'Physical strength'),
											('Invisibility' , 'Become invisible'),
											('Telepotation' , 'Ability to move from one place to another instantaneously');


INSERT INTO SuperheroPower(SuperheroId,PowerId) VALUES(1,1) , (1,2)

INSERT INTO SuperheroPower(SuperheroId,PowerId) VALUES(2,3)

INSERT INTO SuperheroPower(SuperheroId,PowerId) VALUES(3,1) , (3,4)




update Superhero
Set Name = 'ReplacedHero'
Where Id = 3




DELETE FROM Assistant
where Name = 'Assistant1'




SELECT sh.Name as HeroName , p.Name as PowerName, p.Description as PowerDescription
from Superhero sh
INNER JOIN SuperheroPower sp ON sh.Id = sp.SuperheroId
INNER JOIN POWER p on sp.PowerId = p.Id

