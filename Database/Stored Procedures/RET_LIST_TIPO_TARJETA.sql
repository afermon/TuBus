﻿CREATE PROCEDURE [dbo].[RET_LIST_TIPO_TARJETA]
AS
	SELECT * 
	From TBL_TIPOTARJETA
	Where STATUS <> 0
GO
