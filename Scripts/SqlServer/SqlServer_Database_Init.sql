USE [master]
GO
/****** Object:  Database [iLawyer]    Script Date: 2019/8/16 10:10:13 ******/
CREATE DATABASE [iLawyer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'iLawyer', FILENAME = N'E:\DB\iLawyer.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'iLawyer_log', FILENAME = N'E:\DB\iLawyer_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [iLawyer] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [iLawyer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [iLawyer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [iLawyer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [iLawyer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [iLawyer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [iLawyer] SET ARITHABORT OFF 
GO
ALTER DATABASE [iLawyer] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [iLawyer] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [iLawyer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [iLawyer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [iLawyer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [iLawyer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [iLawyer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [iLawyer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [iLawyer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [iLawyer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [iLawyer] SET  DISABLE_BROKER 
GO
ALTER DATABASE [iLawyer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [iLawyer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [iLawyer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [iLawyer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [iLawyer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [iLawyer] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [iLawyer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [iLawyer] SET RECOVERY FULL 
GO
ALTER DATABASE [iLawyer] SET  MULTI_USER 
GO
ALTER DATABASE [iLawyer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [iLawyer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [iLawyer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [iLawyer] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'iLawyer', N'ON'
GO
USE [iLawyer]
GO
/****** Object:  Table [dbo].[Areas]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Areas](
	[AreaCode] [varchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ParentId] [varchar](50) NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[AreaCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ClientPropertyItems]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientPropertyItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[PropertyItemCategoryId] [int] NOT NULL,
	[Value] [nvarchar](200) NOT NULL,
	[OrderNo] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_ClientPropertyItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clients]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsNP] [bit] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ContactNo] [nvarchar](50) NULL,
	[Abbreviation] [nvarchar](50) NULL,
	[Impression] [nvarchar](50) NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Courts]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Courts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Rank] [varchar](50) NOT NULL,
	[Province] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[County] [nvarchar](50) NULL,
	[Address] [nvarchar](200) NULL,
	[ContactNo] [nvarchar](200) NULL,
 CONSTRAINT [PK_Courts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Documents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocType] [varchar](50) NOT NULL,
	[Abstract] [nvarchar](50) NULL,
	[FilePath] [nvarchar](200) NOT NULL,
	[Labels] [nvarchar](200) NULL,
	[Remark] [nvarchar](200) NULL,
	[UploadorId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Judges]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Judges](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactNo] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Gender] [int] NOT NULL,
	[Duty] [nvarchar](50) NULL,
	[Grade] [varchar](50) NULL,
	[CourtId] [int] NOT NULL,
 CONSTRAINT [PK_Judges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectAccounts]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectAccounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TotalAmount] [numeric](18, 2) NULL,
	[RiskBonusPercent] [numeric](18, 2) NULL,
	[ReceivedFee] [numeric](18, 2) NULL,
	[TurnOverFee] [numeric](18, 2) NULL,
	[TurnOverFeePaid] [numeric](18, 2) NULL,
	[Introducer] [nvarchar](50) NULL,
	[IntroduceFee] [numeric](18, 2) NULL,
	[IntroduceFeePaid] [numeric](18, 2) NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_ProjectAccounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectCategories]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectCategories](
	[Code] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[OrderNo] [int] NOT NULL,
	[ParentId] [varchar](50) NULL,
 CONSTRAINT [PK_ProjectCategories] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectCauses]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCauses](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[OrderNo] [int] NOT NULL,
 CONSTRAINT [PK_ProjectCauses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectClients]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectClients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[OrderNo] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ProjectClients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectProgresses]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectProgresses](
	[Id] [varchar](64) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Content] [nvarchar](200) NULL,
	[HandleTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ProjectProgresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryCode] [varchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Level] [varchar](50) NOT NULL,
	[Details] [nvarchar](500) NULL,
	[OtherLitigant] [nvarchar](50) NULL,
	[InterestedParty] [nvarchar](50) NULL,
	[Contacts] [nvarchar](50) NULL,
	[DealDate] [datetime] NOT NULL,
	[OwnerId] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectTodoList]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectTodoList](
	[Id] [varchar](64) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Priority] [int] NOT NULL,
	[IsSetRemind] [bit] NOT NULL,
	[RemindTime] [datetime] NULL,
	[ExpiredTime] [datetime] NULL,
	[Content] [nvarchar](500) NULL,
	[Status] [int] NOT NULL,
	[CompletedTime] [datetime] NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ProjectTodoList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropertyItemCategories]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyItemCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Icon] [nvarchar](50) NULL,
	[PickerType] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_PropertyItemCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemUsers]    Script Date: 2019/8/16 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhoneNo] [varchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Nickname] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
	[Level] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SysUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [iLawyer] SET  READ_WRITE 
GO
