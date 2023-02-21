using Microsoft.Data.SqlClient;
using System.Data;
using TestProjectSQL.Models;
using TestProjectSQL.Repositories;

var chinookRepo = new ChinookRepository { ConnectionString = GetConnectionString() };


/*chinookRepo.Add("Experis");*/

var newCustomer = new Customer(default,"tttest", "Test", "Test", "Test", "Test", "Test");


var customrs = chinookRepo.GetByName("Leoni");

foreach (var customer in customrs)
{
    Console.WriteLine(customer.FirstName);
}
/*DataTable dt = new DataTable();*/

/*dt = chinookRepo.NumberOfCustomerInCountry();


foreach (DataRow row in dt.Rows)
{
    Console.WriteLine(row[0] + " : " +row[1]);
}*/

/*dt = chinookRepo.CustomersHighestSpenders();

foreach(DataRow row in dt.Rows)
{
   Console.WriteLine(row[0] + " : " + row[1]);
}
*/

static string GetConnectionString()
{
    SqlConnectionStringBuilder builder= new SqlConnectionStringBuilder();
    builder.DataSource = "N-SE-01-8034\\SQLEXPRESS01";
    builder.InitialCatalog = "Chinook";
    builder.IntegratedSecurity= true;
    builder.TrustServerCertificate= true;

    return builder.ConnectionString;
}

