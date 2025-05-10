USE [master]
GO

IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = N'BookMyMovieDB'OR name = N'BookMyMovieDB')))
DROP DATABASE BookMyMovieDB
GO

-- Create Database
CREATE DATABASE BookMyMovieDB;
GO
USE BookMyMovieDB;
GO
-- Create Users Table
CREATE TABLE Users (
 UserID INT PRIMARY KEY IDENTITY(1,1),
 FirstName VARCHAR(50) NOT NULL,
 LastName VARCHAR(50) NOT NULL,
 Email VARCHAR(100) UNIQUE NOT NULL,
 ContactNumber VARCHAR(15) UNIQUE NOT NULL
);
GO
-- Create Movies Table
CREATE TABLE Movies (
 MovieID INT PRIMARY KEY IDENTITY(1,1),
 Title VARCHAR(255) NOT NULL,
 Genre VARCHAR(50) NOT NULL,
 Duration INT NOT NULL, -- Duration in minutes
 ReleaseDate DATE NOT NULL
);
GO
-- Create Theaters Table
CREATE TABLE Theaters (
 TheaterID INT PRIMARY KEY IDENTITY(1,1),
 Name VARCHAR(100) NOT NULL,
 Location VARCHAR(255) NOT NULL,
 TotalSeats INT NOT NULL
);
GO

-- Create Bookings Table
CREATE TABLE Bookings (
 BookingID INT PRIMARY KEY IDENTITY(1,1),
 UserID INT FOREIGN KEY REFERENCES Users(UserID) ,
 MovieID INT FOREIGN KEY REFERENCES Movies(MovieID) ,
 TheaterID INT FOREIGN KEY REFERENCES Theaters(TheaterID) ,
 BookingDate DATETIME DEFAULT GETDATE(),
 SeatsBooked INT NOT NULL CHECK (SeatsBooked > 0)
);
GO
-- Insert Sample Data
-- Insert into Users
INSERT INTO Users (FirstName, LastName, Email, ContactNumber) VALUES
('John', 'Doe', 'john.doe@example.com', '1234567890'),
('Jane', 'Smith', 'jane.smith@example.com', '0987654321'),
('Alice', 'Brown', 'alice.brown@example.com', '1122334455'),
('Charlie', 'Davis', 'charlie.davis@example.com', '5566778899'),
('Emma', 'Wilson', 'emma.wilson@example.com', '6677889900');
GO
-- Insert into Movies
INSERT INTO Movies (Title, Genre, Duration, ReleaseDate) VALUES
('Inception', 'Sci-Fi', 148, '2010-07-16'),
('Titanic', 'Romance', 195, '1997-12-19'),
('The Dark Knight', 'Action', 152, '2008-07-18');
GO

-- Insert into Theaters
INSERT INTO Theaters (Name, Location, TotalSeats) VALUES
('Regal Cinemas', 'New York, NY', 200),
('AMC Theaters', 'Los Angeles, CA', 250),
('Cineplex', 'Toronto, ON', 180);
GO

-- Insert into Bookings
INSERT INTO Bookings (UserID, MovieID, TheaterID, SeatsBooked) VALUES
(1, 1, 1, 2),
(2, 2, 2, 3),
(3, 3, 3, 1),
(1, 2, 3, 4),
(2, 3, 1, 2),
(3, 1, 2, 5),
(1, 3, 2, 1),
(2, 1, 3, 3);
GO
