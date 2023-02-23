# SQL-Itunes-Assignment

This repository consist of two projects, where the first one is about creating Superhero database in SQL Server. The second project is a c# console application where we access and manipulate data from SQL Server database.

## Appendix-A : Superhero Db
### Tables
There are three main tables that are created, this is Superhero, Assistant, and Power.
Superhero has: Autoincremented integer Id, Name, Alias, Origin. 
Assistant has: Autoincremented integer Id, Name.
Power has: Autoincremented integer Id, Name, Description.

### Table relationships
- One Superhero can have multiple assistants, one assistant has one superhero they assist.
- One Superhero can have many powers, and one power can be present on many Superheroes.


 ## Appendix-B : Reading data with SQL client
This part of the assignment deals with manipulating SQL Server data in Visual Studio using a library called SQL 
Client. For this part of the assignment, you are given a database to work with. It is called Chinook.

### Chinook Database
The [Chinook data model](https://github.com/lerocha/chinook-database) represents a digital media store, including tables for artists, albums, media tracks, invoices, and customers. Media-related data was created using real data from an Apple iTunes library.


### Requirments
- Visual Studio 2022 with .NET 6
- SQL Server
- Chinook database


 


