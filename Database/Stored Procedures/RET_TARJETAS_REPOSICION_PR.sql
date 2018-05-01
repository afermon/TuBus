CREATE PROCEDURE [dbo].[RET_TARJETAS_REPOSICION_PR]
AS
	Select t.* from TBL_TARJETA as t
		join TBL_ESTADO_TARJETA as e
		on t.ESTADO_TARJETA_ID = e.TBL_ESTADO_TARJETA_ID
		join TBL_TERMINAL as ter 
		on ter.TERMINAL_ID = t.TERMINAL_ID
		where  LOWER(e.NOMBRE_ESTADO) <> 'activo' and   LOWER(ter.ESTADO) <> 'inactivo'  and  LOWER(e.NOMBRE_ESTADO) <> 'inactivo' 
RETURN 0
