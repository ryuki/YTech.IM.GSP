USE [DB_IM_GSP]
GO
/****** Object:  Table [dbo].[M_ROOM]    Script Date: 10/19/2013 02:38:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_ROOM](
	[ROOM_ID] [nvarchar](50) NOT NULL,
	[ROOM_NAME] [nvarchar](50) NULL,
	[ROOM_ORDER_NO] [int] NULL,
	[ROOM_TYPE] [nvarchar](50) NULL,
	[ROOM_STATUS] [nvarchar](50) NULL,
	[ROOM_DESC] [nvarchar](max) NULL,
	[DATA_STATUS] [nvarchar](50) NULL,
	[CREATED_BY] [nvarchar](50) NULL,
	[CREATED_DATE] [datetime] NULL,
	[MODIFIED_BY] [nvarchar](50) NULL,
	[MODIFIED_DATE] [datetime] NULL,
	[ROW_VERSION] [timestamp] NULL,
 CONSTRAINT [PK_M_ROOM] PRIMARY KEY CLUSTERED 
(
	[ROOM_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
