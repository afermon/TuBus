﻿CREATE PROCEDURE [dbo].[RET_ALL_MULTA_PR]
AS
	SELECT TBL_MULTA.*, TBL_EMPRESA.NOMBRE_EMPRESA
	FROM TBL_MULTA
	Inner join TBL_EMPRESA ON TBL_EMPRESA.CEDULA_JURIDICA = EMPRESA
RETURN 0