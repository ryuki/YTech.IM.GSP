USE [DB_IM_GSP]
GO
/****** Object:  Table [dbo].[T_STOCK_CARD]    Script Date: 10/19/2013 02:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_STOCK_CARD](
	[STOCK_CARD_ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ITEM_ID] [nvarchar](50) NOT NULL,
	[WAREHOUSE_ID] [nvarchar](50) NOT NULL,
	[TRANS_DET_ID] [nvarchar](50) NULL,
	[STOCK_CARD_DATE] [datetime] NULL,
	[STOCK_CARD_STATUS] [bit] NULL,
	[STOCK_CARD_QTY] [numeric](18, 5) NULL,
	[STOCK_CARD_SALDO] [numeric](18, 5) NULL,
	[STOCK_CARD_DESC] [nvarchar](max) NULL,
	[DATA_STATUS] [nvarchar](50) NULL,
	[CREATED_BY] [nvarchar](50) NULL,
	[CREATED_DATE] [datetime] NULL,
	[MODIFIED_BY] [nvarchar](50) NULL,
	[MODIFIED_DATE] [datetime] NULL,
	[ROW_VERSION] [timestamp] NULL,
 CONSTRAINT [PK_T_STOCK_CARD_1] PRIMARY KEY CLUSTERED 
(
	[STOCK_CARD_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[T_STOCK_CARD]  WITH CHECK ADD  CONSTRAINT [FK_T_STOCK_CARD_M_ITEM] FOREIGN KEY([ITEM_ID])
REFERENCES [dbo].[M_ITEM] ([ITEM_ID])
GO
ALTER TABLE [dbo].[T_STOCK_CARD] CHECK CONSTRAINT [FK_T_STOCK_CARD_M_ITEM]
GO
ALTER TABLE [dbo].[T_STOCK_CARD]  WITH CHECK ADD  CONSTRAINT [FK_T_STOCK_CARD_M_WAREHOUSE] FOREIGN KEY([WAREHOUSE_ID])
REFERENCES [dbo].[M_WAREHOUSE] ([WAREHOUSE_ID])
GO
ALTER TABLE [dbo].[T_STOCK_CARD] CHECK CONSTRAINT [FK_T_STOCK_CARD_M_WAREHOUSE]
GO
ALTER TABLE [dbo].[T_STOCK_CARD]  WITH CHECK ADD  CONSTRAINT [FK_T_STOCK_CARD_T_TRANS_DET] FOREIGN KEY([TRANS_DET_ID])
REFERENCES [dbo].[T_TRANS_DET] ([TRANS_DET_ID])
GO
ALTER TABLE [dbo].[T_STOCK_CARD] CHECK CONSTRAINT [FK_T_STOCK_CARD_T_TRANS_DET]
GO
