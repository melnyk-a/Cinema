use master;
go

if db_id('Films') is not null
begin
	drop database Films
end
go

create database Films
go

use Films
go

create table Languages
(
	Id int not null identity(1,1) primary key,
	Name nvarchar(50) not null unique
);
go

create table Humans
(
	Id int not null identity(1,1) primary key,
	Name nvarchar(100) not null,
	Surname nvarchar(100) not null
);
go

create table Positions
(
	Id int not null identity(1,1) primary key,
	Name nvarchar(100) not null unique
);
go

create table Films
(
	Id int not null identity(1,1) primary key,
	Title nvarchar(100) not null,
	ReleaseDate date not null,
	LanguageId int not null
);
go

create table HumansFilmsPositions
(
	Id int not null identity(1,1) primary key,
	HumanId int not null,
	PositionId int not null,
	FilmId int not null
);
go

alter table Films
add foreign key (LanguageId) references Languages(Id);
go

alter table HumansFilmsPositions
add foreign key (HumanId) references Humans(Id);
go

alter table HumansFilmsPositions
add foreign key (PositionId) references Positions(Id);
go

alter table HumansFilmsPositions
add foreign key (FilmId) references Films(Id);
go

insert into Languages (Name) values
	(N'Arabic'),
    (N'English'),
    (N'German'),
    (N'Russian'),
    (N'Hindi'),
	(N'Spanish');
go

insert into Humans (Name, Surname) values
	(N'Eli', N'Roth'),
	(N'Jack', N'Black'),
	(N'Cate', N'Blanchett'),
	(N'Owen', N'Vaccaro'),
	(N'Michael', N'Moore'),
	(N'David', N'Hogg'),
	(N'Alexandria', N'Ocasio-Cortez'),
	(N'Dan', N'Fogelman'),
	(N'Oscar', N'Isaac'),
	(N'Olivia', N'Wilde'),
	(N'Annette', N'Bening');
go

insert into Positions (Name) values
	(N'Director'),
	(N'Actor');
go

insert into Films (Title, LanguageId, ReleaseDate) values
	(N'The House with a Clock in Its Walls', 2, '09-27-2018'),
	(N'Fahrenheit 11/9', 2, '09-21-2018'),
	(N'Life Itself', 6, '09-21-2018');
go

insert into HumansFilmsPositions (HumanId, PositionId, FilmId) values 
	(1,1,1),
	(2,2,1),
	(3,2,1),
	(4,2,1),
	(5,1,2),
	(6,2,2),
	(5,2,2),
	(7,2,2),
	(8,1,3),
	(9,2,3),
	(10,2,3),
	(11,2,3);
go