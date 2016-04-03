USE [PassWarder]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 10/10/2014 10:25:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[LoginID] [bigint] NOT NULL,
	[Websait_Name] [varchar](255) NULL,
	[URL] [varchar](255) NULL,
	[Login] [varchar](255) NULL,
	[Password] [varchar](100) NULL,
	[E_mail] [varchar](100) NULL)
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Login_Pass] FOREIGN KEY([LoginID])
REFERENCES [dbo].[Login_Pass] ([Login_PassID])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Login_Pass]
GO