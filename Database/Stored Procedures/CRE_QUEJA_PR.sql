CREATE PROCEDURE [dbo].[CRE_QUEJA_PR]
	@P_Detalle_Queja NVARCHAR (1000),
	@P_Ruta int,
	@P_Placa nvarchar(10) = null,
	@P_Chofer nvarchar(10) = null,
	@P_Hora dateTime,
	@P_Estado nvarchar(10)
AS
	INSERT INTO [dbo].[TBL_QUEJAS] VALUES(@P_Detalle_Queja,@P_Ruta,@P_Chofer, @P_Placa, @P_Hora, @P_Estado);
RETURN 0
