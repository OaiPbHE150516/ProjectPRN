USE [master]
GO
/****** Object:  Database [MoneyWife]    Script Date: 10/25/2022 1:55:00 AM ******/
CREATE DATABASE [MoneyWife]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MoneyWife', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MoneyWife.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MoneyWife_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MoneyWife_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MoneyWife] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MoneyWife].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MoneyWife] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MoneyWife] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MoneyWife] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MoneyWife] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MoneyWife] SET ARITHABORT OFF 
GO
ALTER DATABASE [MoneyWife] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MoneyWife] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MoneyWife] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MoneyWife] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MoneyWife] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MoneyWife] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MoneyWife] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MoneyWife] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MoneyWife] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MoneyWife] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MoneyWife] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MoneyWife] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MoneyWife] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MoneyWife] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MoneyWife] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MoneyWife] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MoneyWife] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MoneyWife] SET RECOVERY FULL 
GO
ALTER DATABASE [MoneyWife] SET  MULTI_USER 
GO
ALTER DATABASE [MoneyWife] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MoneyWife] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MoneyWife] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MoneyWife] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MoneyWife] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MoneyWife] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MoneyWife', N'ON'
GO
ALTER DATABASE [MoneyWife] SET QUERY_STORE = OFF
GO
USE [MoneyWife]
GO
/****** Object:  Table [dbo].[Money]    Script Date: 10/25/2022 1:55:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Money](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[cash] [decimal](18, 0) NULL,
	[bank] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Money] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 10/25/2022 1:55:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[moneyNum] [decimal](18, 0) NOT NULL,
	[cashOrBank] [varchar](50) NOT NULL,
	[moneyContent] [nvarchar](max) NULL,
	[moneyType] [int] NOT NULL,
	[dateUse] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 10/25/2022 1:55:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](250) NULL,
	[category] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/25/2022 1:55:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[fullname] [nvarchar](100) NOT NULL,
	[phone] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[gender] [varchar](100) NULL,
	[location] [varchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Money] ON 

INSERT [dbo].[Money] ([id], [user_id], [cash], [bank]) VALUES (1, 11, CAST(12000 AS Decimal(18, 0)), CAST(240000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Money] OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON 

INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (1, N'Ăn uống', NULL, N'expense')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (3, N'Quần áo', NULL, N'expense')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (4, N'Đi lại', NULL, N'expense')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (5, N'Internet', NULL, N'expense')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (6, N'Tiền nhà', NULL, N'expense')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (7, N'Giáo dục', NULL, N'expense')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (8, N'Y tế', NULL, N'expense')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (9, N'Hiếu hỉ', NULL, N'expense')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (10, N'Giải trí', NULL, N'expense')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (11, N'Khác', NULL, N'expense')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (12, N'Mẹ cho', NULL, N'income')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (13, N'Tiền lương', NULL, N'income')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (14, N'Tiền thưởng', NULL, N'income')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (15, N'Đầu tư', NULL, N'income')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (16, N'Lợi nhuận', NULL, N'income')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (17, N'Thu lãi', NULL, N'income')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (18, N'Cho thuê', NULL, N'income')
INSERT [dbo].[Type] ([id], [name], [description], [category]) VALUES (19, N'Stock', NULL, N'income')
SET IDENTITY_INSERT [dbo].[Type] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id], [username], [password], [fullname], [phone], [email], [gender], [location]) VALUES (1, N'oaipbhe', N'abc123', N'Phạm Bá Oai', N'0838802571', N'oaipbhe150516@fpt.edu.vn', N'male', N'Vietnam')
INSERT [dbo].[Users] ([id], [username], [password], [fullname], [phone], [email], [gender], [location]) VALUES (2, N'oaideptrai', N'abc123', N'Pham Oai', N'', N'', N'other', N'')
INSERT [dbo].[Users] ([id], [username], [password], [fullname], [phone], [email], [gender], [location]) VALUES (3, N'asda', N'abc123', N'Ba Oai', N'', N'', N'other', N'')
INSERT [dbo].[Users] ([id], [username], [password], [fullname], [phone], [email], [gender], [location]) VALUES (4, N'asbc', N'qweqwe', N'jkasdjkashd', N'', N'', N'other', N'')
INSERT [dbo].[Users] ([id], [username], [password], [fullname], [phone], [email], [gender], [location]) VALUES (5, N'sdfsdf', N'eqweqweqw', N'zxcvzc sd', N'', N'', N'other', N'')
INSERT [dbo].[Users] ([id], [username], [password], [fullname], [phone], [email], [gender], [location]) VALUES (6, N'qweqw', N'asdfg', N's czxczx ', N'', N'', N'other', N'')
INSERT [dbo].[Users] ([id], [username], [password], [fullname], [phone], [email], [gender], [location]) VALUES (7, N'werwe', N'abc123', N'sdas dasd', N'', N'', N'other', N'')
INSERT [dbo].[Users] ([id], [username], [password], [fullname], [phone], [email], [gender], [location]) VALUES (8, N'zxczxc', N'123', N'scz xcz ', N'', N'', N'other', N'')
INSERT [dbo].[Users] ([id], [username], [password], [fullname], [phone], [email], [gender], [location]) VALUES (9, N'oaidep', N'123', N'pham ba oai', N'', N'', N'male', N'')
INSERT [dbo].[Users] ([id], [username], [password], [fullname], [phone], [email], [gender], [location]) VALUES (10, N'oai', N'123', N'pham ba oai', N'', N'', N'male', N'')
INSERT [dbo].[Users] ([id], [username], [password], [fullname], [phone], [email], [gender], [location]) VALUES (11, N'oaiba', N'123', N'pham ba oai', N'0812123432', N'', N'male', N'hanoi')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Money]  WITH CHECK ADD  CONSTRAINT [FK_Money_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Money] CHECK CONSTRAINT [FK_Money_Users]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Type] FOREIGN KEY([moneyType])
REFERENCES [dbo].[Type] ([id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Type]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Users]
GO
USE [master]
GO
ALTER DATABASE [MoneyWife] SET  READ_WRITE 
GO
