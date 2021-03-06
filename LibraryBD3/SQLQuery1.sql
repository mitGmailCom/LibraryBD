create database Library3;
use Library3;

CREATE TABLE Author
(
Id INT NOT NULL IDENTITY PRIMARY KEY,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL)

CREATE TABLE Person
(
Id INT NOT NULL IDENTITY PRIMARY KEY,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
Phone int not null)

CREATE TABLE Publisher
(
Id INT NOT NULL IDENTITY PRIMARY KEY,
PublisherName VARCHAR(50) NOT NULL,
Address VARCHAR(100) NOT NULL
)

CREATE TABLE Book
(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Title VARCHAR(50) NOT NULL,
Pages INT,
IdPublisher INT NOT NULL FOREIGN KEY
REFERENCES Publisher(Id)
)

Create table RelationAuthorBook
(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
IdAuthor INT NOT NULL FOREIGN KEY REFERENCES Author(Id),
IdBook INT NOT NULL FOREIGN KEY REFERENCES Book(Id)
)

Create table AuditLibrary
(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
IdPerson INT NOT NULL FOREIGN KEY REFERENCES Person(Id),
IdBook INT NOT NULL FOREIGN KEY REFERENCES Book(Id),
DateIssuance date not null
)
