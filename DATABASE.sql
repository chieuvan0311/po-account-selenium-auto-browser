USE [master]
GO
/****** Object:  Database [PayoneerManager]    Script Date: 11/12/2022 10:05:17 PM ******/
CREATE DATABASE [PayoneerManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PayonnerManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PayonnerManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PayonnerManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PayonnerManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PayoneerManager] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PayoneerManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PayoneerManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PayoneerManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PayoneerManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PayoneerManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PayoneerManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [PayoneerManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PayoneerManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PayoneerManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PayoneerManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PayoneerManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PayoneerManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PayoneerManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PayoneerManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PayoneerManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PayoneerManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PayoneerManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PayoneerManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PayoneerManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PayoneerManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PayoneerManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PayoneerManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PayoneerManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PayoneerManager] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PayoneerManager] SET  MULTI_USER 
GO
ALTER DATABASE [PayoneerManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PayoneerManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PayoneerManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PayoneerManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PayoneerManager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PayoneerManager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PayoneerManager] SET QUERY_STORE = OFF
GO
USE [PayoneerManager]
GO
/****** Object:  User [Chieu311]    Script Date: 11/12/2022 10:05:17 PM ******/
CREATE USER [Chieu311] FOR LOGIN [Chieu311] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 11/12/2022 10:05:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[SessionResult] [nvarchar](500) NULL,
	[AccPassword] [varchar](50) NULL,
	[AccQuestion1] [nvarchar](1000) NULL,
	[AccQuestion2] [nvarchar](1000) NULL,
	[AccQuestion3] [nvarchar](1000) NULL,
	[EmailPassword] [varchar](50) NULL,
	[RecoveryEmail] [varchar](50) NULL,
	[Forward_Email] [varchar](50) NULL,
	[Email_2FA] [varchar](50) NULL,
	[Proxy] [varchar](100) NULL,
	[Profile] [bit] NULL,
	[ProfileID] [varchar](250) NULL,
	[ProfileName] [nvarchar](50) NULL,
	[ProfilePath] [nvarchar](250) NULL,
	[ProfileCreatedTime] [nvarchar](100) NULL,
	[Change_Acc_Info_All] [bit] NULL,
	[Remove_OldRecoveryEmail] [bit] NULL,
	[Add_NewRecoveryEmail] [bit] NULL,
	[Remove_OldForwardEmail] [bit] NULL,
	[Add_NewForwardEmail] [bit] NULL,
	[Change_EmailPassword] [bit] NULL,
	[Change_Email_Info_All] [bit] NULL,
	[Change_AccPassword] [bit] NULL,
	[Add_AccQuestion] [bit] NULL,
	[Up_Link_Status] [bit] NULL,
	[Old_AccPassword] [varchar](500) NULL,
	[Old_EmailPassword] [varchar](500) NULL,
	[Old_RecoveryEmail] [varchar](500) NULL,
	[Old_AccQuestion] [nvarchar](3000) NULL,
	[EmailType] [varchar](50) NULL,
	[AccType] [nvarchar](50) NULL,
	[Random_AccPassword] [varchar](500) NULL,
	[Random_EmailPassword] [varchar](500) NULL,
	[Random_Question] [nvarchar](3000) NULL,
	[Canvas_Profiles] [bit] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 11/12/2022 10:05:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Name] [varchar](50) NOT NULL,
	[Password] [varchar](100) NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Del_Account_Save]    Script Date: 11/12/2022 10:05:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Del_Account_Save](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[SessionResult] [nvarchar](500) NULL,
	[AccPassword] [varchar](50) NULL,
	[AccQuestion1] [nvarchar](1000) NULL,
	[AccQuestion2] [nvarchar](1000) NULL,
	[AccQuestion3] [nvarchar](1000) NULL,
	[EmailPassword] [varchar](50) NULL,
	[RecoveryEmail] [varchar](50) NULL,
	[Forward_Email] [varchar](50) NULL,
	[Email_2FA] [varchar](50) NULL,
	[Proxy] [varchar](100) NULL,
	[Profile] [bit] NULL,
	[ProfileID] [varchar](250) NULL,
	[ProfileName] [nvarchar](50) NULL,
	[ProfilePath] [nvarchar](250) NULL,
	[ProfileCreatedTime] [nvarchar](100) NULL,
	[Change_Acc_Info_All] [bit] NULL,
	[Remove_OldRecoveryEmail] [bit] NULL,
	[Add_NewRecoveryEmail] [bit] NULL,
	[Remove_OldForwardEmail] [bit] NULL,
	[Add_NewForwardEmail] [bit] NULL,
	[Change_EmailPassword] [bit] NULL,
	[Change_Email_Info_All] [bit] NULL,
	[Change_AccPassword] [bit] NULL,
	[Add_AccQuestion] [bit] NULL,
	[Up_Link_Status] [bit] NULL,
	[Old_AccPassword] [varchar](500) NULL,
	[Old_EmailPassword] [varchar](500) NULL,
	[Old_RecoveryEmail] [varchar](500) NULL,
	[Old_AccQuestion] [nvarchar](3000) NULL,
	[EmailType] [varchar](50) NULL,
	[AccType] [nvarchar](50) NULL,
	[Random_AccPassword] [varchar](500) NULL,
	[Random_EmailPassword] [varchar](500) NULL,
	[Random_Question] [nvarchar](3000) NULL,
 CONSTRAINT [PK_Del_Account_Save] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 11/12/2022 10:05:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Se_Question] [nvarchar](500) NULL,
	[Category] [int] NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [PayoneerManager] SET  READ_WRITE 
GO
