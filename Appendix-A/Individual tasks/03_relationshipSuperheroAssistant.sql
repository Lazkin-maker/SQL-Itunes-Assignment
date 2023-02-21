ALTER TABLE Assistant
ADD SuperheroId INT NULL,
CONSTRAINT FK_Assistant_Superhero FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id);