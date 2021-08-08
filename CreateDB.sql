CREATE TABLE `MgZGKoFugz`.`dbo.Departament`(
DepartamentId int auto_increment,
DepartamentName nvarchar(500),
primary key(DepartamentId)
);

insert into `MgZGKoFugz`.`dbo.Departament`(DepartamentName) values ('IT');
insert into `MgZGKoFugz`.`dbo.Departament`(DepartamentName) values ('Support');

CREATE TABLE `MgZGKoFugz`.`dbo.Employee`(
EmployeeId int auto_increment,
EmployeeName nvarchar(500),
Departament nvarchar(500),
DateOfJoining datetime,
PhotoFileName nvarchar(500),
primary key(EmployeeId)
);

insert into `MgZGKoFugz`.`dbo.Employee`(EmployeeName,Departament,DateOfJoining,PhotoFileName)
values ('Bob', 'IT', '2016-01-01', 'anonymous.jpg'),
('John', 'Support', '2016-01-01', 'john.jpg'),
('Mary', 'Support', '2016-01-01', 'mary.jpg'),
('Mike', 'Support', '2016-01-01', 'mike.jpg');