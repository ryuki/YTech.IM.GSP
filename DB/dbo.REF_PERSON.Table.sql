USE [DB_IM_GSP]
GO
/****** Object:  Table [dbo].[REF_PERSON]    Script Date: 10/19/2013 02:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_PERSON](
	[PERSON_ID] [nvarchar](50) NOT NULL,
	[PERSON_FIRST_NAME] [nvarchar](50) NULL,
	[PERSON_LAST_NAME] [nvarchar](50) NULL,
	[PERSON_DOB] [datetime] NULL,
	[PERSON_POB] [nvarchar](50) NULL,
	[PERSON_GENDER] [nvarchar](50) NULL,
	[PERSON_PHONE] [nvarchar](50) NULL,
	[PERSON_MOBILE] [nvarchar](50) NULL,
	[PERSON_EMAIL] [nvarchar](50) NULL,
	[PERSON_RELIGION] [nvarchar](50) NULL,
	[PERSON_RACE] [nvarchar](50) NULL,
	[PERSON_ID_CARD_TYPE] [nvarchar](50) NULL,
	[PERSON_ID_CARD_NO] [nvarchar](50) NULL,
	[PERSON_DESC] [nvarchar](max) NULL,
	[DATA_STATUS] [nvarchar](50) NULL,
	[CREATED_BY] [nvarchar](50) NULL,
	[CREATED_DATE] [nchar](10) NULL,
	[MODIFIED_BY] [nvarchar](50) NULL,
	[MODIFIED_DATE] [datetime] NULL,
	[ROW_VERSION] [timestamp] NULL,
	[PERSON_ANOTHER_NAME] [nvarchar](50) NULL,
	[PERSON_OCCUPATION] [nvarchar](50) NULL,
	[PERSON_OFFICCE_NAME] [nvarchar](50) NULL,
	[PERSON_OFFICCE_ADDRESS] [nvarchar](50) NULL,
	[PERSON_OFFICCE_CITY] [nvarchar](50) NULL,
	[PERSON_OFFICCE_PHONE] [nvarchar](50) NULL,
	[PERSON_OFFICCE_POST_CODE] [nvarchar](50) NULL,
	[PERSON_OFFICCE_FAX] [nvarchar](50) NULL,
	[PERSON_LAST_EDU] [nvarchar](50) NULL,
	[PERSON_MARRIED_STATUS] [nvarchar](50) NULL,
	[PERSON_HOBBY] [nvarchar](50) NULL,
	[PERSON_NATIONALITY] [nvarchar](50) NULL,
	[PERSON_BLOOD_TYPE] [nvarchar](50) NULL,
 CONSTRAINT [PK_REF_PERSON] PRIMARY KEY CLUSTERED 
(
	[PERSON_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
