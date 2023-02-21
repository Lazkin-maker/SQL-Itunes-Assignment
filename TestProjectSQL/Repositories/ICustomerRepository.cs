using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectSQL.Models;

namespace TestProjectSQL.Repositories
{
    public interface ICustomerRepository : ICrudRepository<Customer, int>
    {
        public IEnumerable<Customer> GetByName(string name);
        public IEnumerable<Customer> GetPageOfCustomer(int rows);
        public DataTable NumberOfCustomerInCountry();

        public DataTable CustomersHighestSpenders();
    }
}


