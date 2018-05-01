﻿CREATE TABLE [dbo].[TBL_HORARIO](
	[HORARIO_ID] [int] IDENTITY(1,1) NOT NULL,
	[RUTA_ID] [int] NOT NULL, 
	[DIA_SEMANA] INT NOT NULL,
	[HORA_SALIDA] TIME(0) NOT NULL,
    CONSTRAINT [PK_TBL_HORARIO] PRIMARY KEY ([HORARIO_ID], [RUTA_ID]), 
    CONSTRAINT [FK_TBL_HORARIO_RUTA] FOREIGN KEY ([RUTA_ID]) REFERENCES [TBL_RUTA]([RUTA_ID]),
) 