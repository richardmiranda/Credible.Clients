USE [dev_test]
GO
/****** Object:  User [candidate]    Script Date: 11/20/2018 2:15:16 PM ******/
CREATE USER [candidate] FOR LOGIN [candidate] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [candidate]
GO
/****** Object:  Table [dbo].[u_course_portal]    Script Date: 11/20/2018 2:15:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[u_course_portal](
	[course_portal_id] [int] IDENTITY(1,1) NOT NULL,
	[course_portal_nm] [nvarchar](50) NOT NULL,
	[portal_id] [int] NOT NULL,
 CONSTRAINT [PK_u_course_portal] PRIMARY KEY CLUSTERED 
(
	[course_portal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[u_portal]    Script Date: 11/20/2018 2:15:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[u_portal](
	[portal_id] [int] IDENTITY(1,1) NOT NULL,
	[portal_nm] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_u_portal] PRIMARY KEY CLUSTERED 
(
	[portal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[u_registration]    Script Date: 11/20/2018 2:15:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[u_registration](
	[registration_id] [int] IDENTITY(1,1) NOT NULL,
	[course_portal_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[registration_dttm] [datetime] NOT NULL,
 CONSTRAINT [PK_u_registration] PRIMARY KEY CLUSTERED 
(
	[registration_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[u_user]    Script Date: 11/20/2018 2:15:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[u_user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[first_nm] [nvarchar](50) NOT NULL,
	[last_nm] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_u_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[u_course_portal]  WITH CHECK ADD  CONSTRAINT [FK_Portal_CoursePortal] FOREIGN KEY([portal_id])
REFERENCES [dbo].[u_portal] ([portal_id])
GO
ALTER TABLE [dbo].[u_course_portal] CHECK CONSTRAINT [FK_Portal_CoursePortal]
GO
USE [master]
GO
ALTER DATABASE [dev_test] SET  READ_WRITE 
GO
