﻿CREATE PROCEDURE [dbo].[RET_LINEA_BY_ID]
	@P_LINEA_ID int
AS
	SELECT * from TBL_LINEA
	where ESTADO <> 'Inactivo' and LINEA_ID =  @P_LINEA_ID
RETURN 0
