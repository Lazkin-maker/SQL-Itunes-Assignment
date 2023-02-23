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
    public class CustomerRepository : ICustomerRepository
    {
        public string ConnectionString { get; set; } = string.Empty;

        public IEnumerable<Customer> GetAll()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode,Phone, Email FROM Customer";
            using var command = new SqlCommand(sql, connection);
            using SqlDataReader reader= command.ExecuteReader();

            while (reader.Read())
            {
                yield return new Customer(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.IsDBNull(3) ? null : reader.GetString(3),
                        reader.IsDBNull(4) ? null : reader.GetString(4),
                        reader.IsDBNull(5) ? null : reader.GetString(5),
                        reader.GetString(6)
                    );
            }
        }

        public Customer GetById(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country,PostalCode, Phone, Email FROM Customer WHERE CustomerId = @ID";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", id);
            using SqlDataReader reader = command.ExecuteReader();

            Customer person = new Customer();

            while (reader.Read())
            {
                      person =  new Customer(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.IsDBNull(3) ? null : reader.GetString(3),
                        reader.IsDBNull(4) ? null : reader.GetString(4),
                        reader.IsDBNull(5) ? null : reader.GetString(5),
                        reader.GetString(6)
                    );
            }
            connection.Close();
            return person;
        }

        public IEnumerable<Customer> GetByName(string name)
        {
           
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE FirstName LIKE '%' + @firstname+ '%'";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@firstname", name);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Customer(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.IsDBNull(3) ? null : reader.GetString(3),
                            reader.IsDBNull(4) ? null : reader.GetString(4),
                            reader.IsDBNull(5) ? null : reader.GetString(5),
                            reader.GetString(6)
                        );
                }  
                connection.Close();
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
                        reader.IsDBNull(3) ? null : reader.GetString(3),
                        reader.IsDBNull(4) ? null : reader.GetString(4),
                        reader.IsDBNull(5) ? null : reader.GetString(5),
                        reader.GetString(6)
                    );
            }
            connection.Close();
        }

        public void Add(Customer obj)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                var sql = " INSERT INTO Customer  ( FirstName ,LastName,Country,PostalCode,Phone,Email) VALUES(@FirstName, @LastName, @Country, @PostalCode, @Phone, @Email)";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FirstName", obj.FirstName);
                command.Parameters.AddWithValue("@LastName", obj.LastName);
                command.Parameters.AddWithValue("@Country", obj.Country);
                command.Parameters.AddWithValue("@PostalCode", obj.PostalCode);
                command.Parameters.AddWithValue("@Phone", obj.Phone);
                command.Parameters.AddWithValue("@Email", obj.Email);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            } 
        }
        public void Update(Customer obj)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                var sql = "UPDATE Customer SET FirstName = @firstName, LastName = @lastName, Country = @country, PostalCode = @postalCode, Phone = @phone, Email = @email WHERE CustomerId = @ID";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@firstName", obj.FirstName);
                command.Parameters.AddWithValue("@lastName", obj.LastName);
                command.Parameters.AddWithValue("@country", obj.Country);
                command.Parameters.AddWithValue("@postalCode", obj.PostalCode);
                command.Parameters.AddWithValue("@phone", obj.Phone);
                command.Parameters.AddWithValue("@email", obj.Email);
                command.Parameters.AddWithValue("@ID", obj.CustomerId);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

        public IEnumerable<CustomerGenre> CustomerPopularGenre(int id)
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT TOP (1) WITH TIES Customer.CustomerId, FirstName, LastName, Genre.NAME, COUNT(Genre.NAME) AS GenreNumber");
            stringBuilder.Append(" FROM Customer  INNER JOIN Invoice  ON Customer.CustomerId = Invoice.CustomerId INNER JOIN InvoiceLine  ON Invoice.InvoiceId = InvoiceLine.InvoiceId");
            stringBuilder.Append(" INNER JOIN Track  ON InvoiceLine.TrackId = Track.TrackId  INNER JOIN Genre  ON Track.GenreId = Genre.GenreId");
            stringBuilder.Append(" WHERE [Customer].[CustomerId] = @ID");
            stringBuilder.Append(" GROUP BY Customer.CustomerId, FirstName, LastName,Genre.NAME");
            stringBuilder.AppendLine(" Order by GenreNumber DESC");
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = stringBuilder.ToString();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", id);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                yield return new CustomerGenre(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetInt32(4)
                    );
            }
        }


    }
}
