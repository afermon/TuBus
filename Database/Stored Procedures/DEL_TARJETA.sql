CREATE PROCEDURE DEL_TARJETA
    @P_CODIGO_UNICO nvarchar(50)                                              
AS 
	Begin
		SET NOCOUNT ON;  
		UPDATE  TBL_TARJETA
		SET           
		   ESTADO_TARJETA_ID = (Select et.TBL_ESTADO_TARJETA_ID 
								from TBL_ESTADO_TARJETA as et 
								where et.NOMBRE_ESTADO = 'Inactivo')
		   where CODIGO_UNICO = @P_CODIGO_UNICO
	End
GO  
