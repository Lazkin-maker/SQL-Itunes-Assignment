using Microsoft.Data.SqlClient;
using System.Data;
using TestProjectSQL.Models;
using TestProjectSQL.Repositories;

var chinookRepo = new CustomerRepository { ConnectionString = GetConnectionString() };


// Task 1
/*var customers = chinookRepo.GetAll();

foreach(var customer in customers)
{
    Console.WriteLine(customer);
}*/

// Task 2
/*var customerById = chinookRepo.GetById(34);
foreach(var customer in customerById)
{
    Console.WriteLine(customer);
}
*/

//Task 3
/*var customerByName = chinookRepo.GetByName("João");
foreach (var customer in customerByName)
{
    Console.WriteLine(customer);
}*/


// Task 4
/*var pages = chinookRepo.GetPageOfCustomer(5, 10);

foreach (var page in pages)
{
    Console.WriteLine(page);
}*/


// Task 5
/*var newCustomer = new Customer(default, "tttest", "Test", "Test", "Test", "Test", "Test");
bool checkCustomerInsert = chinookRepo.Add(newCustomer);
if (!checkCustomerInsert)
{
    throw new Exception(nameof(checkCustomerInsert) + ": Returns fail!");
}*/

// Task 6
/*var UpdateCustomer = new Customer(64, "Updated", "Updated", "Updated", "Updated", "Updated", "Updated");
bool checkIfUpdated = chinookRepo.Update(UpdateCustomer);
if (!checkIfUpdated)
{
    throw new Exception(nameof(UpdateCustomer) + "Returns fail!");
}*/

// Task 7
/*var customerInCountry = chinookRepo.NumberOfCustomerInCountry();
foreach (var obj in customerInCountry)
{
    Console.WriteLine(obj.country + ": " + obj.count);
}
*/

// Task 8
/*var customrs = chinookRepo.CustomersHighestSpenders();

foreach (var customer in customrs)
{
    Console.WriteLine(customer.firstName + " : " + customer.total);
}*/


// Task 9
var popularGenre = chinookRepo.CustomerPopularGenre("Roberto");

foreach(var record in popularGenre)
{
    Console.WriteLine(record);
}








static string GetConnectionString()
{
    SqlConnectionStringBuilder builder= new SqlConnectionStringBuilder();
    builder.DataSource = "N-SE-01-8034\\SQLEXPRESS01";
    builder.InitialCatalog = "Chinook";
    builder.IntegratedSecurity= true;
    builder.TrustServerCertificate= true;

    return builder.ConnectionString;
}

