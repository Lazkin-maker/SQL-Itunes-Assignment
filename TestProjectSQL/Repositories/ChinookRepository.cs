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
    public class ChinookRepository
    {
        public string ConnectionString { get; set; } = string.Empty;

        public IEnumerable<Customer> CustomerGetAll()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer";
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
            var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE CustomerId = @ID";
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
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.GetString(6)
                        );
                }
            
        }


        public IEnumerable<Customer> GetPageOfCustomer(int rows)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer ORDER BY CustomerId OFFSET 0 ROWS FETCH FIRST @ROWS ROWS ONLY";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ROWS", rows);

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

        public bool InsertCustomer(Customer customer)
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
            
            if(cmd == 0)
            {
                isInsert = false;
            }
            else
            {
                isInsert = true;
            }
            return isInsert;           
        }


        public DataTable NumberOfCustomerInCountry()
        {
            DataTable dataTable = new DataTable();
            try
            { 
                using var conn = new SqlConnection(ConnectionString);
                conn.Open();
                var sql = "Select c.Country, Count(*) from Customer as c group by c.Country order by count(C.Country) DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand= cmd;
                dataAdapter.Fill(dataTable);
                conn.Close();
                dataAdapter.Dispose();

               
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return dataTable;
        }


        public DataTable CustomersHighestSpenders()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                conn.Open();
                var sql = "select c.FirstName ,i.Total from Customer as c INNER JOIN Invoice as i on c.CustomerId = i.CustomerId group by i.Total, c.FirstName order by i.total DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand= cmd;
                dataAdapter.Fill(dataTable);
                conn.Close(); 
                dataAdapter.Dispose();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTable;
        }


    }
}
