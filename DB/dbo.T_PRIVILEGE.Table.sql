USE [DB_IM_GSP]
GO
/****** Object:  Table [dbo].[T_PRIVILEGE]    Script Date: 10/19/2013 02:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_PRIVILEGE](
	[PRIVILEGE_ID] [nvarchar](50) NOT NULL,
	[PRIVILEGE_TYPE] [nvarchar](50) NULL,
	[USER_NAME] [nvarchar](50) NULL,
	[MENU_ID] [nvarchar](50) NULL,
	[PRIVILEGE_STATUS] [bit] NULL,
	[PRIVILEGE_DESC] [nvarchar](50) NULL,
	[DATA_STATUS] [nvarchar](50) NULL,
	[CREATED_BY] [nvarchar](50) NULL,
	[CREATED_DATE] [datetime] NULL,
	[MODIFIED_BY] [nvarchar](50) NULL,
	[MODIFIED_DATE] [datetime] NULL,
	[ROW_VERSION] [timestamp] NULL,
 CONSTRAINT [PK_T_PRIVILEGE] PRIMARY KEY CLUSTERED 
(
	[PRIVILEGE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[T_PRIVILEGE]  WITH CHECK ADD  CONSTRAINT [FK_T_PRIVILEGE_M_MENU] FOREIGN KEY([MENU_ID])
REFERENCES [dbo].[M_MENU] ([MENU_ID])
GO
ALTER TABLE [dbo].[T_PRIVILEGE] CHECK CONSTRAINT [FK_T_PRIVILEGE_M_MENU]
GO
