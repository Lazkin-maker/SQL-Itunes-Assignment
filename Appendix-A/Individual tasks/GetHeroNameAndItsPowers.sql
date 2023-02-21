SELECT sh.Name as HeroName , p.Name as PowerName, p.Description as PowerDescription
from Superhero sh
INNER JOIN SuperheroPower sp ON sh.Id = sp.SuperheroId
INNER JOIN POWER p on sp.PowerId = p.Id

