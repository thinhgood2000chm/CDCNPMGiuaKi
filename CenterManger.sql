USE [master]
GO
/****** Object:  Database [CenterManager]    Script Date: 06/12/2021 23:27:17 ******/
CREATE DATABASE [CenterManager]

GO
ALTER DATABASE [CenterManager] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CenterManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CenterManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CenterManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CenterManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CenterManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CenterManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [CenterManager] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CenterManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CenterManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CenterManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CenterManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CenterManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CenterManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CenterManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CenterManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CenterManager] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CenterManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CenterManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CenterManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CenterManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CenterManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CenterManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CenterManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CenterManager] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CenterManager] SET  MULTI_USER 
GO
ALTER DATABASE [CenterManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CenterManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CenterManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CenterManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CenterManager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CenterManager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CenterManager', N'ON'
GO
ALTER DATABASE [CenterManager] SET QUERY_STORE = OFF
GO
USE [CenterManager]
GO
/****** Object:  Table [dbo].[class]    Script Date: 06/12/2021 23:27:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[class](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[class_id] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NULL,
	[subject_id] [nvarchar](50) NULL,
	[teacher_id] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[class_student]    Script Date: 06/12/2021 23:27:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[class_student](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [nvarchar](50) NULL,
	[class_id] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[student]    Script Date: 06/12/2021 23:27:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[student](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NULL,
	[birthYear] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subject]    Script Date: 06/12/2021 23:27:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subject](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[subject_id] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[teacher]    Script Date: 06/12/2021 23:27:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[teacher](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[teacher_id] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userLogin]    Script Date: 06/12/2021 23:27:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userLogin](
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[token] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[class] ON 

INSERT [dbo].[class] ([id], [class_id], [name], [subject_id], [teacher_id]) VALUES (1, N'180502', N'Chuyên đề N1', N'01', N'001')
INSERT [dbo].[class] ([id], [class_id], [name], [subject_id], [teacher_id]) VALUES (2, N'180503', N'Phân tích N1', N'02', N'002')
INSERT [dbo].[class] ([id], [class_id], [name], [subject_id], [teacher_id]) VALUES (3, N'180504', N'Thiết kế N1', N'03', N'003')
INSERT [dbo].[class] ([id], [class_id], [name], [subject_id], [teacher_id]) VALUES (4, N'180505', N'Web ứng dụng N1', N'04', N'004')
INSERT [dbo].[class] ([id], [class_id], [name], [subject_id], [teacher_id]) VALUES (5, N'180506', N'Web nâng cao N1', N'05', N'005')
INSERT [dbo].[class] ([id], [class_id], [name], [subject_id], [teacher_id]) VALUES (6, N'180507', N'C cơ bản N1', N'07', N'006')
INSERT [dbo].[class] ([id], [class_id], [name], [subject_id], [teacher_id]) VALUES (7, N'180508', N'QP3 N2', N'06', N'007')
SET IDENTITY_INSERT [dbo].[class] OFF
GO
SET IDENTITY_INSERT [dbo].[class_student] ON 

INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (1, N'51800022', N'180502')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (2, N'51800123', N'180502')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (3, N'51800015', N'180502')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (4, N'51800015', N'180503')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (5, N'51800025', N'180502')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (6, N'51800023', N'180502')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (7, N'51800789', N'180502')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (8, N'51800763', N'180502')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (9, N'51800399', N'180502')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (10, N'51800035', N'180502')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (11, N'51800105', N'180502')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (12, N'51800022', N'180508')
INSERT [dbo].[class_student] ([id], [student_id], [class_id]) VALUES (14, N'51800123', N'180508')
SET IDENTITY_INSERT [dbo].[class_student] OFF
GO
SET IDENTITY_INSERT [dbo].[student] ON 

INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (1, N'51800022', N'Phạm Hồng Hải Đăng', 2000)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (2, N'51800123', N'Nguyễn Xuân Thịnh', 2000)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (3, N'51800015', N'Lê Thanh Bình', 2000)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (4, N'51800025', N'Phạm Phú Di', 2000)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (5, N'51800023', N'Ngô Tấn Đạt', 2000)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (6, N'51800789', N'Nguyễn Trần Quỳnh Như', 1999)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (7, N'51800763', N'Nguyễn Minh Khoa', 2000)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (8, N'51800399', N'Nguyễn Tiến Dũng', 2000)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (9, N'51800035', N'Phạm Gia Hân', 2000)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (10, N'51800105', N'Trương Minh Quang', 2000)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (11, N'51800044', N'Trương Minh Hưng', 2000)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (12, N'51800154', N'Trần Thị Kim Tuyến', 2000)
INSERT [dbo].[student] ([id], [student_id], [name], [birthYear]) VALUES (13, N'51600001', N'Nguyễn Phúc Thùy Tiên', 1998)
SET IDENTITY_INSERT [dbo].[student] OFF
GO
SET IDENTITY_INSERT [dbo].[subject] ON 

INSERT [dbo].[subject] ([id], [subject_id], [name]) VALUES (1, N'01', N'Chuyên đề')
INSERT [dbo].[subject] ([id], [subject_id], [name]) VALUES (2, N'02', N'Phân tích')
INSERT [dbo].[subject] ([id], [subject_id], [name]) VALUES (3, N'03', N'Thiết kế')
INSERT [dbo].[subject] ([id], [subject_id], [name]) VALUES (4, N'04', N'Toán')
INSERT [dbo].[subject] ([id], [subject_id], [name]) VALUES (5, N'05', N'QP1')
INSERT [dbo].[subject] ([id], [subject_id], [name]) VALUES (6, N'06', N'QP2')
INSERT [dbo].[subject] ([id], [subject_id], [name]) VALUES (7, N'07', N'QP3')
SET IDENTITY_INSERT [dbo].[subject] OFF
GO
SET IDENTITY_INSERT [dbo].[teacher] ON 

INSERT [dbo].[teacher] ([id], [teacher_id], [name]) VALUES (1, N'001', N'Thầy Hồng')
INSERT [dbo].[teacher] ([id], [teacher_id], [name]) VALUES (2, N'002', N'Thầy Nhân')
INSERT [dbo].[teacher] ([id], [teacher_id], [name]) VALUES (3, N'003', N'Thầy Trung')
INSERT [dbo].[teacher] ([id], [teacher_id], [name]) VALUES (4, N'004', N'Thầy Mạnh')
INSERT [dbo].[teacher] ([id], [teacher_id], [name]) VALUES (5, N'005', N'Thầy Hòa')
INSERT [dbo].[teacher] ([id], [teacher_id], [name]) VALUES (6, N'006', N'Cô Hoàng Anh')
INSERT [dbo].[teacher] ([id], [teacher_id], [name]) VALUES (7, N'007', N'Cô Thùy Tiên')
SET IDENTITY_INSERT [dbo].[teacher] OFF
GO
INSERT [dbo].[userLogin] ([username], [password], [token]) VALUES (N'admin', N'admin', NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__class__FDF4798716F35D7B]    Script Date: 06/12/2021 23:27:17 ******/
ALTER TABLE [dbo].[class] ADD UNIQUE NONCLUSTERED 
(
	[class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__student__2A33069BE18E078F]    Script Date: 06/12/2021 23:27:17 ******/
ALTER TABLE [dbo].[student] ADD UNIQUE NONCLUSTERED 
(
	[student_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__subject__5004F661E109EC8B]    Script Date: 06/12/2021 23:27:17 ******/
ALTER TABLE [dbo].[subject] ADD UNIQUE NONCLUSTERED 
(
	[subject_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__teacher__03AE777FDCE9671E]    Script Date: 06/12/2021 23:27:17 ******/
ALTER TABLE [dbo].[teacher] ADD UNIQUE NONCLUSTERED 
(
	[teacher_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [CenterManager] SET  READ_WRITE 
GO
