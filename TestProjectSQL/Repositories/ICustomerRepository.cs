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
        /// <summary>
        /// Retrieves a particular instance from the database by its name.
        /// </summary>
        /// <param name="name">Firstname of customer</param>
        /// <returns>Returns instance of customer</returns>
        public IEnumerable<Customer> GetByName(string name);

        /// <summary>
        /// Retrieves list countries with amount Of customers for each country.
        /// </summary>
        /// <returns>Returns IEnumerable containing Customers</returns>
        public IEnumerable<CustomerCountry> NumberOfCustomerInCountry();


        // <summary>
        /// Retrieves list of customers who are the highest spenders.
        /// </summary>
        /// <returns>Returns IEnumerable containing Customers spenders</returns>
        public IEnumerable<CustomerSpender> CustomersHighestSpenders();


        /// <summary>
        /// Retrieves limited list off instances with offset from the database.
        /// </summary>
        /// <returns>Returns IEnumerable containing page of customers</returns>
        public IEnumerable<Customer> GetPageOfCustomer(int limit, int offset);


        /// <summary>
        /// Retrieves most popular genre of a given customer.
        /// </summary>
        /// <returns>Returns IEnumerable containing customer with favourite genre</returns>
        public IEnumerable<CustomerGenre> CustomerPopularGenre(int id);
    }
}


