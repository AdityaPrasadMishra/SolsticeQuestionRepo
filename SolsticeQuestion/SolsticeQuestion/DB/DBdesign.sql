/*****USe your own DB Name*****/
USE [ContactsDB]
GO

/****** Object: Table [dbo].[ContactItems] Script Date: 09-06-2019 14:38:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[ContactItems];


GO
CREATE TABLE [dbo].[ContactItems] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (MAX) NULL,
    [Company]         NVARCHAR (50)  NULL,
    [BirthDate]       DATETIME2 (7)  NULL,
    [HomePhoneNumber] NVARCHAR (15)  NULL,
    [WorkPhoneNumber] NVARCHAR (15)  NULL,
    [City]            NVARCHAR (15)  NULL,
    [State]           NVARCHAR (15)  NULL,
    [Address]         NVARCHAR (MAX) NULL,
    [ProfileImage]    NVARCHAR (50)  NULL,
    [Email]           NVARCHAR (50)  NULL
);



