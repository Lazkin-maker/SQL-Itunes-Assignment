using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectSQL.Models
{
    public readonly record struct CustomerGenre(int Id, string FirstName , string LastName,string GenreName, int Counts);
    
    
}
