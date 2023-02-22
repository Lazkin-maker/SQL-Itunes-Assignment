using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectSQL.Models;

namespace TestProjectSQL.Repositories
{
    public class CustomerRepository
    {
        public string ConnectionString { get; set; } = string.Empty;

        public IEnumerable<Customer> GetAll()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, ISNULL(PostalCode, 'null'), ISNULL(Phone, 'null'), Email FROM Customer";
            using var command = new SqlCommand(sql, connection);
            using SqlDataReader reader= command.ExecuteReader();

            while (reader.Read())
            {
                yield return new Customer(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6)
                    );
            }
        }

        public IEnumerable<Customer> GetById(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, ISNULL(PostalCode, 'null'), ISNULL(Phone, 'null'), Email FROM Customer WHERE CustomerId = @ID";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", id);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new Customer(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6)
                    );
            }
        }

        public IEnumerable<Customer> GetByName(string name)
        {
           
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                var sql = "SELECT CustomerId, FirstName, LastName, Country,  ISNULL(PostalCode, 'null'), ISNULL(Phone, 'null'), Email FROM Customer WHERE FirstName LIKE '%' + @firstname+ '%'";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@firstname", name);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Customer(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.GetString(6)
                        );
                }       
        }
        public IEnumerable<Customer> GetPageOfCustomer(int limit, int offset)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer ORDER BY CustomerId OFFSET @Offset ROWS FETCH FIRST @Rows ROWS ONLY";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Rows", limit);
            command.Parameters.AddWithValue("@Offset", offset);


            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new Customer(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6)
                    );
            }
        }

        public bool Add(Customer customer)
        {
            bool isInsert = false;

            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var sql = "INSERT INTO Customer (FirstName, LastName, Country, PostalCode, Phone, Email) VALUES(@FIRSTNAME, @LASTNAME, @COUNTRY, @POSTALCODE, @PHONE, @EMAIL)";

            using var command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@FIRSTNAME", customer.FirstName);
            command.Parameters.AddWithValue("@LASTNAME", customer.LastName);
            command.Parameters.AddWithValue("@COUNTRY", customer.Country);
            command.Parameters.AddWithValue("@POSTALCODE", customer.PostalCode);
            command.Parameters.AddWithValue("@PHONE", customer.Phone);
            command.Parameters.AddWithValue("@EMAIL", customer.Email);

            int cmd = command.ExecuteNonQuery();          
            if(cmd == 1)
            {
                isInsert = false;
            }
            else
            {
                isInsert = true;
            }
            return isInsert;           
        }
        public bool Update(Customer customer)
        {
            bool isUpdate = false;
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();

            var sql = "update Customer set FirstName =@firstname, LastName =@lastname, Country =@country, PostalCode =@postalcode, Phone =@phone, Email =@email where CustomerId = @ID";
            using var command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@firstname", customer.FirstName);
            command.Parameters.AddWithValue("@lastname", customer.LastName);
            command.Parameters.AddWithValue("@country", customer.Country);
            command.Parameters.AddWithValue("@postalcode", customer.PostalCode);
            command.Parameters.AddWithValue("@phone", customer.Phone);
            command.Parameters.AddWithValue("@email", customer.Email);
            command.Parameters.AddWithValue("@ID", customer.CustomerId);

            int cmd = command.ExecuteNonQuery();

            if (cmd == 0)
            {
                isUpdate = false;
            }
            else
            {
                isUpdate = true;
            }
            return isUpdate;
        }



        public IEnumerable<CustomerCountry> NumberOfCustomerInCountry()
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var sql = "Select c.Country, Count(*) from Customer as c group by c.Country order by count(C.Country) DESC";

            SqlCommand cmd = new SqlCommand(sql, conn);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                yield return new CustomerCountry(
                    reader.GetString(0),
                    reader.GetInt32(1)
                    );
            }
        }
        public IEnumerable<CustomerSpender> CustomersHighestSpenders() {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var sql = "select c.FirstName ,i.Total from Customer as c INNER JOIN Invoice as i on c.CustomerId = i.CustomerId group by i.Total, c.FirstName order by i.total DESC";

            SqlCommand cmd = new SqlCommand(sql, conn);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                yield return new CustomerSpender(
                    reader.GetString(0),
                    reader.GetDecimal(1)
                    );
            }
        }

        public IEnumerable<CustomerGenre> CustomerPopularGenre(string firstname)
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var sql = "SELECT c.FirstName, g.Name, COUNT(*) as Counts FROM Genre AS g INNER JOIN Track AS t ON t.GenreId = g.GenreId INNER JOIN InvoiceLine AS il ON il.TrackId = t.TrackId INNER JOIN Invoice AS i ON i.InvoiceId = il.InvoiceId INNER JOIN Customer AS c ON c.CustomerId = i.CustomerId WHERE c.FirstName = @firstname GROUP BY c.FirstName, g.Name HAVING COUNT(*) = ( SELECT MAX(cnt)  FROM ( SELECT COUNT(*) AS cnt  FROM Genre AS g INNER JOIN Track AS t ON t.GenreId = g.GenreId INNER JOIN InvoiceLine AS il ON il.TrackId = t.TrackId INNER JOIN Invoice AS i ON i.InvoiceId = il.InvoiceId INNER JOIN Customer AS c ON c.CustomerId = i.CustomerId WHERE c.FirstName = @firstname GROUP BY c.FirstName, g.Name ) AS counts)";
            
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                yield return new CustomerGenre(
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetInt32(2)
                    );
            }
        }


    }
}
