﻿CREATE TABLE [dbo].[TBL_LINEASANCION](
	[LINEA_SANCION_ID] [int] IDENTITY(1,1) NOT NULL,
	[FECHA_SANCION] [date] NOT NULL,
	[SANCION_ID] [int] NOT NULL,
	[LINEA_ID] [int] NOT NULL,
	CONSTRAINT [PK_TBL_LINEASANCION_LINEA_SANCION_ID] PRIMARY KEY CLUSTERED ([LINEA_SANCION_ID] ASC),
	CONSTRAINT [TBL_LINEASANCION$FK_LINEASANCION_SANCION1] FOREIGN KEY([SANCION_ID]) REFERENCES [dbo].[TBL_SANCION] ([SANCION_ID]),
	CONSTRAINT [TBL_LINEASANCION$FK_LINEASANCION_LINEA1] FOREIGN KEY([LINEA_ID]) REFERENCES [dbo].[TBL_LINEA] ([LINEA_ID])
)