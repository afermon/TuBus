CREATE PROCEDURE [dbo].[DEL_RUTA_PR]
	@P_RUTA_ID int
AS
		Update [dbo].[TBL_RUTA]
	set
		ESTADO = 'Inactivo'
     WHERE [RUTA_ID] = @P_RUTA_ID
RETURN 0
