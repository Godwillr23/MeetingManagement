CREATE DATABASE [MeetingTrackDB]
GO

USE [MeetingManagementDB]
GO

CREATE TABLE [dbo].[MeetingType](
	[MeetingTypeId] INT PRIMARY KEY IDENTITY,
	[MeetingType] [varchar](50) NOT NULL
)

CREATE TABLE [dbo].[Meeting](
	[MeetingId] INT PRIMARY KEY IDENTITY,
	[MeetingTypeId] [int] NOT NULL,
	[MeetingCode] [varchar](15) NOT NULL,
	[MeetingDate] [varchar](15) NOT NULL,
	[MeetingTime] [varchar](15) NOT NULL,
	[DateCreated] datetime NOT NULL
)

CREATE TABLE [dbo].[Staff](
	[StaffId] INT PRIMARY KEY IDENTITY,
	[Firstname] [varchar](15) NOT NULL,
	[Lastname] [varchar](15) NOT NULL
)

CREATE TABLE [dbo].[MeetingItems](
	[ItemId] INT PRIMARY KEY IDENTITY,
	[MeetingId] [int] NOT NULL,
	[MeetingItemName] [varchar](50) NOT NULL,
	[ActionBy] [varchar](50) NOT NULL,
	[ItemDescription] text NOT NULL,
	[ItemStatus] [varchar](30) NOT NULL,
	[DateCreated] datetime NOT NULL,
	[DueDate] [varchar](30) NOT NULL
)

CREATE TABLE [dbo].[MeetingStatus](
	[StatusItemId] INT PRIMARY KEY IDENTITY,
	[ItemId] [int] NOT NULL,
	[Status] [varchar](100) NOT NULL
)

CREATE TABLE [dbo].[Status](
	[StatusId] INT PRIMARY KEY IDENTITY,
	[Status] [varchar](100) NOT NULL
)

--update-database -verbose -force