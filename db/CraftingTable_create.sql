CREATE DATABASE CraftingTable
GO
USE CraftingTable
GO

CREATE TABLE Boards (
	 Id uniqueidentifier PRIMARY KEY,
	 ProjectId uniqueidentifier  NOT NULL,
	 Name nvarchar (255) NOT NULL,
	 CreationTime datetime  NULL
	 )

CREATE TABLE Users (
	 Id uniqueidentifier PRIMARY KEY,
	 Name nvarchar (255) NOT NULL,
	 Email nvarchar (255) NOT NULL,
	 Active bit NOT NULL,
	 CreationTime datetime NOT NULL,
	 )

CREATE TABLE BoardColumns (
	 Id uniqueidentifier PRIMARY KEY,
	 BoardId uniqueidentifier REFERENCES Boards NOT NULL,
	 CreatedBy uniqueidentifier REFERENCES Users NOT NULL,
	 Name nvarchar (255) NOT NULL,
	 CreationTime datetime NOT NULL,
	 )

CREATE TABLE Contributors (
	 ProjectId uniqueidentifier PRIMARY KEY,
	 ContributorId uniqueidentifier REFERENCES Users NOT NULL,
	 Role nvarchar (255) NOT NULL
	 )

CREATE TABLE Projects (
	 Id uniqueidentifier PRIMARY KEY,
	 CreatedBy uniqueidentifier REFERENCES Users NOT NULL,
	 Name nvarchar (255) NOT NULL,
	 Description nvarchar (1000) NULL,
	 CreationTime datetime NOT NULL,
	 EstimateUnit nvarchar (50) NULL,
	 DefaultBackgroundColor nvarchar (50) NOT NULL
	 )

CREATE TABLE Tasks (
	 Id uniqueidentifier PRIMARY KEY,
	 BoardColumnId uniqueidentifier REFERENCES BoardColumns NOT NULL,
	 AssignedTo uniqueidentifier REFERENCES Users  NULL,
	 CreatedBy uniqueidentifier REFERENCES Users NOT NULL,
	 LastModifiedBy uniqueidentifier REFERENCES Users NOT NULL,
	 Name nvarchar (255) NOT NULL,
	 TaskType nvarchar (50) NOT NULL,
	 Description nvarchar (1000) NULL,
	 Notes ntext NULL,
	 PessimisticEstimate int NULL,
	 OptimisticEstimate int NULL,
	 RealisticEstimate int NULL,
	 [Rank] int NOT NULL,
	 BackgroundColor nvarchar (50) NOT NULL,
	 CreationTime datetime NOT NULL,
	 LastModifiedTime datetime NOT NULL
	 )