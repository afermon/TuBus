﻿CREATE TABLE [dbo].[TBL_REGISTROPARQUEO](
	[REGISTRO_PARQUEO_ID] INT NOT NULL IDENTITY, 
	[TERMINAL_ID] [int] NOT NULL,
	[COSTO_TOTAL] [int] NULL DEFAULT (NULL),
	[HORA_INICIO] DATETIME NULL DEFAULT (NULL),
	[HORA_FIN] DATETIME NULL  DEFAULT (NULL),
	[TARJETA_ID] [nvarchar](50) NOT NULL
    CONSTRAINT [PK_TBL_ESPACIOPARQUEO] PRIMARY KEY ([REGISTRO_PARQUEO_ID]), 
	CONSTRAINT [TBL_ESPACIOPARQUEO$FK_CARROPARQUEO_PARQUEPUBLICO] FOREIGN KEY([TERMINAL_ID]) REFERENCES [dbo].[TBL_TERMINAL] ([TERMINAL_ID]),
	CONSTRAINT [TBL_ESPACIOPARQUEO$FK_TBL_CARROPARQUEO_TBL_TARJETA] FOREIGN KEY([TARJETA_ID])REFERENCES [dbo].[TBL_TARJETA] ([CODIGO_UNICO])
) ON [PRIMARY]
GO