USE [master]
GO
/****** Object:  Database [dbo.Album]    Script Date: 06.10.2020 23:50:42 ******/
CREATE DATABASE [dbo.Album]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbo.Album', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\dbo.Album.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbo.Album_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\dbo.Album_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [dbo.Album] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbo.Album].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbo.Album] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbo.Album] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbo.Album] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbo.Album] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbo.Album] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbo.Album] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbo.Album] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbo.Album] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbo.Album] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbo.Album] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbo.Album] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbo.Album] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbo.Album] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbo.Album] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbo.Album] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbo.Album] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbo.Album] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbo.Album] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbo.Album] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbo.Album] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbo.Album] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbo.Album] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbo.Album] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbo.Album] SET  MULTI_USER 
GO
ALTER DATABASE [dbo.Album] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbo.Album] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbo.Album] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbo.Album] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbo.Album] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbo.Album] SET QUERY_STORE = OFF
GO
USE [dbo.Album]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [uniqueidentifier] NOT NULL,
	[PhotoId] [uniqueidentifier] NOT NULL,
	[AuthorId] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photos]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photos](
	[Id] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](45) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhotoTagAssocs]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhotoTagAssocs](
	[PhotoId] [uniqueidentifier] NOT NULL,
	[TagId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Regards]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regards](
	[Id] [uniqueidentifier] NOT NULL,
	[PhotoId] [uniqueidentifier] NOT NULL,
	[AuthorId] [uniqueidentifier] NOT NULL,
	[Rating] [int] NOT NULL,
 CONSTRAINT [PK_Regards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[Id] [uniqueidentifier] NOT NULL,
	[TagName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Login] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](32) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admins] ADD  CONSTRAINT [DF_Admins_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Comments] ADD  CONSTRAINT [DF_Comments_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Comments] ADD  CONSTRAINT [DF_Comments_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Photos] ADD  CONSTRAINT [DF_Photos_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Photos] ADD  CONSTRAINT [DF_Photos_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Regards] ADD  CONSTRAINT [DF_Regards_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Tags] ADD  CONSTRAINT [DF_Tags_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Admins]  WITH CHECK ADD  CONSTRAINT [FK_Admins_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Admins] CHECK CONSTRAINT [FK_Admins_Users]
GO
ALTER TABLE [dbo].[Photos]  WITH CHECK ADD  CONSTRAINT [FK_Photos_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Photos] CHECK CONSTRAINT [FK_Photos_Users]
GO
ALTER TABLE [dbo].[PhotoTagAssocs]  WITH CHECK ADD  CONSTRAINT [FK_PhotoTagAssocs_Photos] FOREIGN KEY([PhotoId])
REFERENCES [dbo].[Photos] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhotoTagAssocs] CHECK CONSTRAINT [FK_PhotoTagAssocs_Photos]
GO
ALTER TABLE [dbo].[PhotoTagAssocs]  WITH CHECK ADD  CONSTRAINT [FK_PhotoTagAssocs_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhotoTagAssocs] CHECK CONSTRAINT [FK_PhotoTagAssocs_Tags]
GO
ALTER TABLE [dbo].[Regards]  WITH CHECK ADD  CONSTRAINT [FK_Regards_Photos] FOREIGN KEY([PhotoId])
REFERENCES [dbo].[Photos] ([Id])
GO
ALTER TABLE [dbo].[Regards] CHECK CONSTRAINT [FK_Regards_Photos]
GO
ALTER TABLE [dbo].[Regards]  WITH CHECK ADD  CONSTRAINT [FK_Regards_Users] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Regards] CHECK CONSTRAINT [FK_Regards_Users]
GO
/****** Object:  StoredProcedure [dbo].[Album_AddTagToPhoto]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_AddTagToPhoto]
	@PhotoId uniqueidentifier,
	@TagId uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	INSERT INTO PhotoTagAssocs(PhotoId, TagId)
	VALUES (@PhotoId, @TagId)
END
GO
/****** Object:  StoredProcedure [dbo].[Album_AddUserToAdmins]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_AddUserToAdmins]
	@UserId uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	INSERT INTO Admins(UserId)
	VALUES (@UserId)
END
GO
/****** Object:  StoredProcedure [dbo].[Album_DeleteCommentById]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_DeleteCommentById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE FROM Comments
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Album_DeletePhotoById]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_DeletePhotoById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE FROM Photos
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Album_DeleteRegardById]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_DeleteRegardById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE FROM Regards
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Album_DeleteTagById]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_DeleteTagById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE FROM Tags
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Album_DeleteTagFromPhoto]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_DeleteTagFromPhoto]
	@PhotoId uniqueidentifier,
	@TagId uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE
	FROM PhotoTagAssocs
	WHERE PhotoId = @PhotoId AND TagId = @TagId
END
GO
/****** Object:  StoredProcedure [dbo].[Album_DeleteUserById]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_DeleteUserById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE
	FROM Users
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetAllUsers]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetAllUsers]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM Users
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetAvgRatingByPhotoId]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetAvgRatingByPhotoId]
	@PhotoId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT AVG(CAST(Rating as float))
	FROM Regards
	WHERE Regards.PhotoId = @PhotoId
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetCommentById]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetCommentById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT *
	FROM Comments
	WHERE Comments.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetCommentsByPhotoId]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetCommentsByPhotoId]
	@PhotoId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT *
	FROM Comments
	WHERE Comments.PhotoId = @PhotoId
	ORDER BY Comments.Date DESC
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetCommentsByUserId]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetCommentsByUserId]
	@UserId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT *
	FROM Comments
	WHERE Comments.AuthorId = @UserId
	ORDER BY Comments.Date DESC
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetPhotoById]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetPhotoById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT *
	FROM Photos
	WHERE Photos.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetPhotoByTag]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetPhotoByTag]
	@TagName nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT Photos.*
	FROM (Photos JOIN PhotoTagAssocs ON (Photos.Id = PhotoTagAssocs.PhotoId)) JOIN Tags ON (Tags.Id = PhotoTagAssocs.TagId)
	WHERE Tags.TagName = @TagName
	ORDER BY Photos.Date DESC
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetPhotosByUserId]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetPhotosByUserId]
	@UserId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM Photos
	WHERE UserId = @UserId
	ORDER BY Photos.Date DESC
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetRatingByPhotoIdUserId]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetRatingByPhotoIdUserId]
	@UserId uniqueidentifier,
	@PhotoId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT Rating
	FROM Regards
	WHERE Regards.PhotoId = @PhotoId AND Regards.AuthorId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetRatingByPhotoIdUserLogin]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetRatingByPhotoIdUserLogin]
	@UserLogin nvarchar(20),
	@PhotoId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT Rating
	FROM Regards JOIN Users ON (Regards.AuthorId = Users.Id)
	WHERE Regards.PhotoId = @PhotoId AND Users.Login = @UserLogin
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetRegardsByPhotoId]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetRegardsByPhotoId]
	@PhotoId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT *
	FROM Regards
	WHERE Regards.PhotoId = @PhotoId
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetRegardsByUserId]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetRegardsByUserId]
	@UserId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT *
	FROM Regards
	WHERE Regards.AuthorId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetTagByName]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetTagByName]
	@TagName nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM Tags
	WHERE Tags.TagName = @TagName
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetTagsByPhotoId]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetTagsByPhotoId]
	@PhotoId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT Tags.*
	FROM (Photos JOIN PhotoTagAssocs ON (Photos.Id = PhotoTagAssocs.PhotoId)) JOIN Tags ON (PhotoTagAssocs.TagId = Tags.Id)
	WHERE Photos.Id = @PhotoId
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetTagsContainString]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetTagsContainString]
	@SubString nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	Select TOP 10 *
	FROM Tags
	WHERE Tags.TagName LIKE '%'+@SubString+'%' ;
END

GO
/****** Object:  StoredProcedure [dbo].[Album_GetTagsStartingAt]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetTagsStartingAt]
	@SubString nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 10 *
	FROM Tags
	WHERE Tags.TagName LIKE @SubString+'%' ;
END

GO
/****** Object:  StoredProcedure [dbo].[Album_GetUserById]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetUserById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM Users
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Album_GetUserByLogin]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_GetUserByLogin]
	@Login nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM Users
	WHERE Login = @Login
END
GO
/****** Object:  StoredProcedure [dbo].[Album_InsertComment]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_InsertComment]
	@PhotoId uniqueidentifier,
	@AuthorId uniqueidentifier,
	@Text nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT OFF;

	INSERT INTO Comments(PhotoId, AuthorId, Text)
	VALUES (@PhotoId, @AuthorId, @Text)
END
GO
/****** Object:  StoredProcedure [dbo].[Album_InsertPhoto]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_InsertPhoto]
	@Id uniqueidentifier,
	@FileName nvarchar(45),
	@UserId uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	INSERT INTO Photos(Id, Filename, UserId)
	VALUES (@Id, @FileName, @UserId)
END
GO
/****** Object:  StoredProcedure [dbo].[Album_InsertRegard]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_InsertRegard]
	@PhotoId uniqueidentifier,
	@AuthorId uniqueidentifier,
	@Rating smallint
AS
BEGIN
	SET NOCOUNT OFF;

	INSERT INTO Regards(PhotoId, AuthorId, Rating)
	VALUES (@PhotoId, @AuthorId, @Rating)
END
GO
/****** Object:  StoredProcedure [dbo].[Album_InsertTag]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_InsertTag]
	@Id uniqueidentifier = NULL,
	@TagName nvarchar(20)
AS
BEGIN
	SET NOCOUNT OFF;

	INSERT INTO Tags (Id, TagName)
	VALUES (@Id, @TagName)
END
GO
/****** Object:  StoredProcedure [dbo].[Album_InsertUser]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_InsertUser]
	@Login nvarchar(20),
	@Password nvarchar(32) = NULL,
	@Name nvarchar(50),
	@Avatar nvarchar(MAX) = NULL
AS
BEGIN
	SET NOCOUNT OFF;

    INSERT INTO Users (Login, Password, Name, Avatar)
	VALUES (@Login, CONVERT(NVARCHAR(32),HashBytes('MD5', @Password),2), @Name, @Avatar)
END
GO
/****** Object:  StoredProcedure [dbo].[Album_IsAccountExist]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_IsAccountExist]
	@Login nvarchar(20),
	@Password nvarchar(32)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT COUNT(*)
	FROM Users
	WHERE Login = @Login AND Password = CONVERT(NVARCHAR(32),HashBytes('MD5', @Password),2)
END
GO
/****** Object:  StoredProcedure [dbo].[Album_IsTagInUse]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_IsTagInUse]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT COUNT(*)
	FROM PhotoTagAssocs
	WHERE PhotoTagAssocs.TagId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Album_IsUserActive]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_IsUserActive]
	@Login nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT COUNT(*)
	FROM Users
	WHERE Login = @Login AND Active = 1
END
GO
/****** Object:  StoredProcedure [dbo].[Album_IsUserAdmin]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_IsUserAdmin]
	@Login nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT COUNT(*)
	FROM Admins JOIN Users ON (Users.Id = Admins.UserId)
	WHERE Users.Login = @Login
END
GO
/****** Object:  StoredProcedure [dbo].[Album_MostCommentedPhotos]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_MostCommentedPhotos]
	
AS
BEGIN
	SET NOCOUNT ON;

    SELECT TOP 20 Photos.*
	FROM Photos LEFT JOIN Comments ON (Photos.Id = Comments.PhotoId) 
	GROUP BY Photos.Id, Photos.Date, Photos.Filename, Photos.UserId
	ORDER BY COUNT(Comments.Id) DESC
END
GO
/****** Object:  StoredProcedure [dbo].[Album_MostRatedPhotos]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_MostRatedPhotos]
	
AS
BEGIN
	SET NOCOUNT ON;

    SELECT TOP 20 Photos.*
	FROM Photos LEFT JOIN Regards ON (Photos.Id = Regards.PhotoId)
	GROUP BY Photos.Id, Photos.Date, Photos.FileName, Photos.UserId
	ORDER BY AVG(CAST(Regards.Rating as float)) DESC
END
GO
/****** Object:  StoredProcedure [dbo].[Album_MostRegardsCountPhotos]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_MostRegardsCountPhotos]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 20 Photos.*
	FROM Photos LEFT JOIN Regards ON (Photos.Id = Regards.PhotoId) 
	GROUP BY Photos.Id, Photos.Date, Photos.Filename, Photos.UserId
	ORDER BY COUNT(Regards.Id) DESC
END
GO
/****** Object:  StoredProcedure [dbo].[Album_RemoveTagFromPhoto]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_RemoveTagFromPhoto]
	@PhotoId uniqueidentifier,
	@TagId uniqueidentifier
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE
	FROM PhotoTagAssocs
	WHERE PhotoId = @PhotoId AND TagId = @TagId
END
GO
/****** Object:  StoredProcedure [dbo].[Album_SetUserPassword]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_SetUserPassword]
	@Id uniqueidentifier,
	@Password nvarchar(32)
AS
BEGIN
	SET NOCOUNT OFF;

	UPDATE Users
	SET Password = CONVERT(NVARCHAR(32),HashBytes('MD5', @Password),2)
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Album_UpdateUserById]    Script Date: 06.10.2020 23:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Album_UpdateUserById]
	@id uniqueidentifier,
	@Name nvarchar(50),
	@Password nvarchar(32),
	@Avatar nvarchar(MAX) = NULL,
	@Active bit
AS
BEGIN
	SET NOCOUNT OFF;

    UPDATE Users
	SET Name = @Name, Password = @Password, Avatar = @Avatar, Active = @Active
	WHERE Id = @Id
END
GO
USE [master]
GO
ALTER DATABASE [dbo.Album] SET  READ_WRITE 
GO
