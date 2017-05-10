USE [master]
GO
/****** Object:  Database [LiveGraph1]    Script Date: 5/10/2017 12:13:33 PM ******/
CREATE DATABASE [LiveGraph1]

ALTER DATABASE [LiveGraph1] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LiveGraph1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LiveGraph1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LiveGraph1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LiveGraph1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LiveGraph1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LiveGraph1] SET ARITHABORT OFF 
GO
ALTER DATABASE [LiveGraph1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LiveGraph1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LiveGraph1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LiveGraph1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LiveGraph1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LiveGraph1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LiveGraph1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LiveGraph1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LiveGraph1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LiveGraph1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LiveGraph1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LiveGraph1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LiveGraph1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LiveGraph1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LiveGraph1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LiveGraph1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LiveGraph1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LiveGraph1] SET RECOVERY FULL 
GO
ALTER DATABASE [LiveGraph1] SET  MULTI_USER 
GO
ALTER DATABASE [LiveGraph1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LiveGraph1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LiveGraph1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LiveGraph1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LiveGraph1] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'LiveGraph', N'ON'
GO
ALTER DATABASE [LiveGraph1] SET QUERY_STORE = OFF
GO
USE [LiveGraph1]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [LiveGraph1]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[Id] [uniqueidentifier] NOT NULL,
	[Loggin] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](20) NOT NULL,
	[Password] [binary](512) NOT NULL,
 CONSTRAINT [PK_AppUser_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AppUsers_Claim]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUsers_Claim](
	[Id_User] [uniqueidentifier] NOT NULL,
	[Id_Claim] [int] NOT NULL,
 CONSTRAINT [PK_AppUser_Claim] PRIMARY KEY CLUSTERED 
(
	[Id_User] ASC,
	[Id_Claim] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Claim]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Claim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Claim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pages]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Text] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[AppUsers_Claim]  WITH CHECK ADD  CONSTRAINT [FK_AppUser_Claim_AppUser_Claim] FOREIGN KEY([Id_User])
REFERENCES [dbo].[AppUser] ([Id])
GO
ALTER TABLE [dbo].[AppUsers_Claim] CHECK CONSTRAINT [FK_AppUser_Claim_AppUser_Claim]
GO
ALTER TABLE [dbo].[AppUsers_Claim]  WITH CHECK ADD  CONSTRAINT [FK_AppUsers_Claim_Claim] FOREIGN KEY([Id_Claim])
REFERENCES [dbo].[Claim] ([Id])
GO
ALTER TABLE [dbo].[AppUsers_Claim] CHECK CONSTRAINT [FK_AppUsers_Claim_Claim]
GO
/****** Object:  StoredProcedure [dbo].[AddPages]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddPages]
@Name nvarchar(50),
@Text nvarchar(max),
@Description nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Pages ([Name], [Text],[Description])
	VALUES (@Name, @Text, @Description)
END

GO
/****** Object:  StoredProcedure [dbo].[AppUserLoggin]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AppUserLoggin] 
@Login nvarchar(20),
@Password binary(512)
AS
BEGIN
	SELECT A.Id FROM AppUser as A WHERE A.Loggin = @Login AND A.[Password] = @Password;
END

GO
/****** Object:  StoredProcedure [dbo].[AppUserRegistration]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AppUserRegistration] 
@Password binary(512),
@Email nvarchar(20),
@Login nvarchar(20) 
AS
BEGIN
DECLARE @Id uniqueidentifier = NEWID();
	BEGIN TRAN
		INSERT INTO [AppUser] ([Email],[Password],[Loggin],[Id])
		VALUES (@Email, @Password, @Login, @Id)

		INSERT INTO AppUsers_Claim (Id_Claim,Id_User) 
		VALUES (1,@Id)
	COMMIT
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllPages]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllPages]
	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT P.Id ,P.[Name], P.[Description],P.[Text] FROM Pages P
END

GO
/****** Object:  StoredProcedure [dbo].[GetClaims]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[GetClaims]
	@Id uniqueidentifier
AS
BEGIN
	SELECT  CONVERT(nvarchar(20), C.Id) AS [VALUE] ,C.[Name] AS [Type] FROM AppUsers_Claim A JOIN Claim C ON A.Id_Claim = C.Id WHERE A.Id_User = @Id;
END

GO
/****** Object:  StoredProcedure [dbo].[GetNameAppUserById]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetNameAppUserById]
	 @Id uniqueidentifier
AS
BEGIN
	SELECT A.Loggin FROM AppUser A WHERE A.Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[GetPageById]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetPageById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT P.Id ,P.[Name], P.[Description],P.[Text] FROM Pages P
	WHERE P.Id = @id
END

GO
/****** Object:  StoredProcedure [dbo].[IsUserExists]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IsUserExists]
@Email nvarchar(20),
@Login nvarchar(20),
@IsUserExislEmail bit= 0 OUTPUT,
@IsUserExistsLogin bit = 0 OUTPUT 
AS
BEGIN
SET @IsUserExistsLogin  = 0;
SET @IsUserExislEmail = 0;

	SELECT @IsUserExislEmail = 1 FROM AppUser A WHERE A.Email = @Email
	SELECT @IsUserExistsLogin = 1 FROM AppUser A WHERE A.Loggin = @Login
END

GO
/****** Object:  StoredProcedure [dbo].[UpdatePages]    Script Date: 5/10/2017 12:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[UpdatePages]
@Id int,
@Name nvarchar(50),
@Text nvarchar(max),
@Description nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE  Pages
	SET  Pages.[Name] = @Name,
	Pages.[Description] = @Description,
	Pages.[Text] = @Text
	WHERE Pages.Id =@Id
END

GO
USE [master]
GO
ALTER DATABASE [LiveGraph1] SET  READ_WRITE 
GO
