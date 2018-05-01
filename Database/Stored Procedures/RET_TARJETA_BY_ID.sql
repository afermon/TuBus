CREATE PROCEDURE [dbo].[RET_TARJETA_BY_ID]
@P_CODIGO_UNICO  Nvarchar(50)                                      
AS 
	Begin
		SET NOCOUNT ON;  
		Select t.* from TBL_TARJETA as t
		join TBL_ESTADO_TARJETA as e
		on t.ESTADO_TARJETA_ID = e.TBL_ESTADO_TARJETA_ID
		join TBL_TERMINAL as ter 
		on ter.TERMINAL_ID = t.TERMINAL_ID
		where Lower(e.NOMBRE_ESTADO) <> 'inactivo' and t.CODIGO_UNICO = @P_CODIGO_UNICO and  lower(ter.ESTADO) <> 'inactivo'
	End
GO  