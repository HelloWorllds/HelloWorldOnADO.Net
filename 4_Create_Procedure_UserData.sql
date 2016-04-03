USE [PassWarder]
GO

GO
CREATE PROCEDURE UserData @LoginID bigint, @Websait_Name varchar(255), @URL varchar(255), @Login varchar(255), @Password varchar(100), @E_mail varchar(100)
AS
INSERT INTO Users (LoginID, Websait_Name, URL, Login, Password, E_mail)
VALUES (@LoginID, @Websait_Name, @URL, @Login, @Password, @E_mail)