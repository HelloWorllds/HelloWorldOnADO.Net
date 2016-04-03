USE [PassWarder]
GO

GO
CREATE PROCEDURE RegData @Login varchar(100), @Password varchar(255)
AS
INSERT INTO Login_Pass VALUES (@Login, @Password)
