CREATE PROCEDURE CRE_TARJETA
    @P_CODIGO_UNICO nvarchar(50),
	@P_SALDO_DISPONIBLE decimal(18,4),
	@P_TERMINAL_ID int,
	@P_USUARIO nvarchar(50),
	@P_TIPOTARJETA_ID int,
	@P_CONVENIO_ID int = null,
	@P_ESTADO_TARJETA_ID int                                                 
AS 
	Begin
		SET NOCOUNT ON;  
		INSERT INTO TBL_TARJETA
          ( 
           CODIGO_UNICO,
		   SALDO_DISPONIBLE,
		   TERMINAL_ID,
		   USUARIO,
		   TIPOTARJETA_ID,
		   CONVENIO_ID,
		   ESTADO_TARJETA_ID                                  
          ) 
     VALUES 
          ( 
            @P_CODIGO_UNICO,
		    @P_SALDO_DISPONIBLE,
			@P_TERMINAL_ID ,
			@P_USUARIO ,
			@P_TIPOTARJETA_ID ,
			@P_CONVENIO_ID ,
			@P_ESTADO_TARJETA_ID                                                 
          ) 	
	End
GO  
