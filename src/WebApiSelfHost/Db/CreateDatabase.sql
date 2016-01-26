IF EXISTS (select name from master.dbo.sysdatabases WHERE name = N'CraftingTable')
DROP DATABASE CraftingTable;
GO

CREATE DATABASE CraftingTable
GO

USE CraftingTable
GO

CREATE TABLE AppUsers (
	 Id uniqueidentifier PRIMARY KEY,
	 Name nvarchar (255) NOT NULL,
	 Email nvarchar (255) NOT NULL,
	 Active bit NOT NULL,
	 CreationTime datetime NOT NULL,
	 )
GO

CREATE TABLE Projects (
	 Id uniqueidentifier PRIMARY KEY,
	 CreatedBy uniqueidentifier REFERENCES AppUsers NOT NULL,
	 Name nvarchar (255) NOT NULL,
	 Description nvarchar (1000) NULL,
	 CreationTime datetime NOT NULL,
	 EstimateUnit nvarchar (50) NULL,
	 DefaultBackgroundColor nvarchar (50) NOT NULL
	 )
GO

CREATE TABLE Boards (
	 Id uniqueidentifier PRIMARY KEY,
	 ProjectId uniqueidentifier  NOT NULL,
	 Name nvarchar (255) NOT NULL,
	 CreationTime datetime  NULL
	 )
GO

CREATE TABLE BoardColumns (
	 Id uniqueidentifier PRIMARY KEY,
	 BoardId uniqueidentifier REFERENCES Boards NOT NULL,
	 CreatedBy uniqueidentifier REFERENCES AppUsers NOT NULL,
	 Name nvarchar (255) NOT NULL,
	 CreationTime datetime NOT NULL,
	 )
GO

CREATE TABLE Contributors (
	 ProjectId uniqueidentifier PRIMARY KEY,
	 ContributorId uniqueidentifier REFERENCES AppUsers NOT NULL,
	 Role nvarchar (255) NOT NULL
	 )
GO

CREATE TABLE Tasks (
	 Id uniqueidentifier PRIMARY KEY,
	 BoardColumnId uniqueidentifier REFERENCES BoardColumns NOT NULL,
	 AssignedTo uniqueidentifier REFERENCES AppUsers  NULL,
	 CreatedBy uniqueidentifier REFERENCES AppUsers NOT NULL,
	 LastModifiedBy uniqueidentifier REFERENCES AppUsers NOT NULL,
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
GO