CREATE DATABASE [DB_RockContent]
GO
--
USE [DB_RockContent]
GO
--
IF OBJECT_ID('[dbo].[Article]') IS NOT NULL
DROP TABLE [dbo].[Article]
GO
CREATE TABLE [dbo].[Article]
(
	Id int identity(1,1) primary key,
	Creation datetime,
	Content varchar(100)
)
GO
--
IF OBJECT_ID('[dbo].[Like]') IS NOT NULL
DROP TABLE [dbo].[Like]
GO
CREATE TABLE [dbo].[Like]
(
	Id int identity(1,1) primary key,
	Creation datetime,
	UserCode varchar(15),
	ArticleId int
)
GO
--
INSERT INTO Article values(getutcdate(),'Primer Articulo')
INSERT INTO Article values(getutcdate(),'Segundo Articulo')
INSERT INTO Article values(getutcdate(),'Tercer Articulo')
GO