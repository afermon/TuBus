﻿CREATE PROCEDURE [dbo].[DEL_CHOFER_PR]
	@P_CEDULA nvarchar(9)

AS
	UPDATE TBL_CHOFER

	SET ESTADO = 'Inactivo'

	where CEDULA = @P_CEDULA
RETURN 0