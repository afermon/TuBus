CREATE PROCEDURE [dbo].[RET_CHOFER_BY_RUTA_PR]
	@P_RUTA_ID INT

AS 
	SELECT tc.* FROM TBL_CHOFER as tc
	inner join TBL_LINEA tl on tl.EMPRESA_ID = tc.EMPRESA
	inner join TBL_RUTA tr on tr.LINEA_ID = tl.LINEA_ID
	where tr.RUTA_ID = @P_RUTA_ID
	
return 0
