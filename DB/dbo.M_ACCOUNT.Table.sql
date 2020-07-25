USE [DB_IM_GSP]
GO
/****** Object:  Table [dbo].[M_ACCOUNT]    Script Date: 10/19/2013 02:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_ACCOUNT](
	[ACCOUNT_ID] [nvarchar](50) NOT NULL,
	[ACCOUNT_CAT_ID] [nvarchar](50) NULL,
	[ACCOUNT_PARENT_ID] [nvarchar](50) NULL,
	[ACCOUNT_STATUS] [nvarchar](50) NULL,
	[ACCOUNT_NAME] [nvarchar](50) NULL,
	[ACCOUNT_DESC] [nvarchar](max) NULL,
	[DATA_STATUS] [nvarchar](50) NULL,
	[CREATED_BY] [nvarchar](50) NULL,
	[CREATED_DATE] [datetime] NULL,
	[MODIFIED_BY] [nvarchar](50) NULL,
	[MODIFIED_DATE] [datetime] NULL,
	[ROW_VERSION] [timestamp] NULL,
 CONSTRAINT [PK_M_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[M_ACCOUNT]  WITH CHECK ADD  CONSTRAINT [FK_M_ACCOUNT_M_ACCOUNT] FOREIGN KEY([ACCOUNT_PARENT_ID])
REFERENCES [dbo].[M_ACCOUNT] ([ACCOUNT_ID])
GO
ALTER TABLE [dbo].[M_ACCOUNT] CHECK CONSTRAINT [FK_M_ACCOUNT_M_ACCOUNT]
GO
ALTER TABLE [dbo].[M_ACCOUNT]  WITH CHECK ADD  CONSTRAINT [FK_M_ACCOUNT_M_ACCOUNT_CAT] FOREIGN KEY([ACCOUNT_CAT_ID])
REFERENCES [dbo].[M_ACCOUNT_CAT] ([ACCOUNT_CAT_ID])
GO
ALTER TABLE [dbo].[M_ACCOUNT] CHECK CONSTRAINT [FK_M_ACCOUNT_M_ACCOUNT_CAT]
GO
