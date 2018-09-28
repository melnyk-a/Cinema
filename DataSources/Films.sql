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

create table Actors
(
Id int not null identity(1,1) primary key,
HumanId int not null
);
go

create table Directors
(
Id int not null identity(1,1) primary key,
HumanId int not null
);
go

create table Films
(
	Id int not null identity(1,1) primary key,
	Title nvarchar(100) not null,
	ReleaseDate datetime not null,
	LanguageId int not null
);
go

create table ActorsFilms
(
Id int not null identity(1,1) primary key,
ActorId int not null,
FilmId int not null
);
go

create table DirectorsFilms
(
Id int not null identity(1,1) primary key,
DirectorId int not null,
FilmId int not null
);
go

alter table Actors
add foreign key (HumanId) references Humans(Id);
go

alter table Directors
add foreign key (HumanId) references Humans(Id);
go

alter table Films
add foreign key (LanguageId) references Languages(Id);
go

alter table ActorsFilms
add foreign key (ActorId) references Actors(Id);
go

alter table ActorsFilms
add foreign key (FilmId) references Films(Id);
go

alter table DirectorsFilms
add foreign key (DirectorId) references Directors(Id);
go

alter table DirectorsFilms
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


insert into Films (Title, LanguageId, ReleaseDate) values
	(N'The House with a Clock in Its Walls', 2, '09-27-2018'),
	(N'Fahrenheit 11/9', 2, '09-21-2018'),
	(N'Life Itself', 6, '09-21-2018');
go

insert into Actors (HumanId) values
	(2),
	(3),
	(4),
	(5),
	(6),
	(7),
	(9),
	(10),
	(11);
go

insert into Directors(HumanId) values
	(1),
	(5),
	(8);
go

insert into ActorsFilms(ActorId, FilmId) values
	(1, 1),
	(2, 1),
	(3, 1),
	(4, 2),
	(5, 2),
	(6, 2),
	(7, 3),
	(8, 3),
	(9, 1),
	(9, 3);
go

insert into DirectorsFilms(DirectorId, FilmId) values
	(1, 1),
	(2, 2),
	(3, 3);
go

create or alter procedure AddFilm @title nvarchar(100), @releaseDate dateTime, @languageId int
as
begin
	insert into Films(Title, ReleaseDate, LanguageId) values
	(@title, @releaseDate , @languageId)
	return  ident_current('Films');
end
go

create or alter procedure AddActor @name nvarchar(100), @surname nvarchar(100), @filmId int
as
begin
	begin transaction
	begin try
		begin
			insert into Humans(Name, Surname) values
			(@name, @surname);
			insert into Actors(HumanId) values
			(ident_current('Humans'));
			insert into ActorsFilms(ActorId, FilmId) values
			(ident_current('Actors'), @filmId);
			commit transaction;
		end
	end try
	begin catch
		begin
			rollback  transaction;
			raiserror('Incorrect actor data', 3, 1);
		end
	end catch
end
go

create or alter procedure AddDirector @name nvarchar(100), @surname nvarchar(100), @filmId int
as
begin
	begin transaction
	begin try
		begin
			insert into Humans(Name, Surname) values
			(@name, @surname);
			insert into Directors(HumanId) values
			(ident_current('Humans'));
			insert into DirectorsFilms(DirectorId, FilmId) values
			(ident_current('Directors'), @filmId);
			commit transaction;
		end
	end try
	begin catch
		begin
			rollback  transaction;
			raiserror('Incorrect director data', 3, 1);
		end
	end catch
end
go