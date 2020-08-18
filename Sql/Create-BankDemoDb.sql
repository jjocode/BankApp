USE [BankDemoDb]
GO

/****** Object:  Table [dbo].[Transactions]    Script Date: 5/15/2020 2:28:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromAccountId] [int] NOT NULL,
	[FromName] [varchar] (100),
	[ToAccountId] [int] NOT  NULL,
	[ToName] [varchar] (100),
	[TransactionDate] [datetime2](7) NOT NULL,
	[TransactionAmount] [decimal](18, 2) NOT NULL,
	[FromBalance] [decimal](18, 2) NOT NULL,
	[ToBalance] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

)

GO


