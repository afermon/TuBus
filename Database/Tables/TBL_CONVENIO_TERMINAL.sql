﻿CREATE TABLE [dbo].[TBL_CONVENIO_TERMINAL]
(
	[Id_CONVENIO_TERMINAL] [int] IDENTITY(1,1) NOT NULL, 
    [TERMINAL_ID] INT NOT NULL, 
    [CEDULA_JURIDICA] INT NOT NULL, 
    CONSTRAINT [PK_TBL_CONVENIO_TERMINAL] PRIMARY KEY ([Id_CONVENIO_TERMINAL])
)