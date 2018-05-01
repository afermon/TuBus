CREATE PROCEDURE [dbo].[UPD_SALDO_TARJETA]
    @P_CODIGO_UNICO nvarchar(50),
	@P_SALDO_DISPONIBLE float                                                
AS 
	Begin
		SET NOCOUNT ON;  
		UPDATE  TBL_TARJETA
		SET           
		   SALDO_DISPONIBLE =  @P_SALDO_DISPONIBLE
		   where CODIGO_UNICO = @P_CODIGO_UNICO
	End
GO  
