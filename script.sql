USE [Atlas]
GO
/****** Object:  Table [dbo].[city]    Script Date: 25.05.2024 10:15:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[city](
	[id] [int] NOT NULL,
	[name] [nvarchar](100) NULL,
	[region] [nvarchar](20) NULL,
 CONSTRAINT [PK_city] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[department]    Script Date: 25.05.2024 10:15:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[department](
	[dep_id] [int] NOT NULL,
	[uni_id] [int] NOT NULL,
	[faculty] [varchar](max) NOT NULL,
	[department] [varchar](max) NOT NULL,
	[uni_time] [int] NOT NULL,
	[dep_type] [varchar](max) NOT NULL,
	[kont] [varchar](max) NOT NULL,
	[siralama] [varchar](max) NULL,
	[base] [float] NULL,
 CONSTRAINT [PK_department] PRIMARY KEY CLUSTERED 
(
	[dep_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[uni]    Script Date: 25.05.2024 10:15:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[uni](
	[uni_id] [int] NOT NULL,
	[uni_name] [nvarchar](max) NULL,
	[city_id] [int] NULL,
	[type] [nvarchar](max) NULL,
	[url] [nvarchar](max) NULL,
 CONSTRAINT [PK_uni] PRIMARY KEY CLUSTERED 
(
	[uni_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[department]  WITH CHECK ADD  CONSTRAINT [FK_department_uni] FOREIGN KEY([uni_id])
REFERENCES [dbo].[uni] ([uni_id])
GO
ALTER TABLE [dbo].[department] CHECK CONSTRAINT [FK_department_uni]
GO
ALTER TABLE [dbo].[uni]  WITH CHECK ADD  CONSTRAINT [FK_uni_city] FOREIGN KEY([city_id])
REFERENCES [dbo].[city] ([id])
GO
ALTER TABLE [dbo].[uni] CHECK CONSTRAINT [FK_uni_city]
GO
