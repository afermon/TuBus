CREATE PROCEDURE [dbo].[CRE_LINEA_PR]
	@P_NOMBRE_LINEA Nvarchar(45),
	@P_TERMINAL_ID int,
	@P_EMPRESA_ID int,
	@P_ESPACIOS_PARQUEO int
AS 
	Begin
		SET NOCOUNT ON;  
		INSERT INTO TBL_LINEA
          ( 
			NOMBRE_LINEA,
			ESTADO,
			TERMINAL_ID,
			EMPRESA_ID,
			ESPACIOS_PARQUEO
          ) 
     VALUES 
          ( 
            @P_NOMBRE_LINEA,
			'Activo',
			@P_TERMINAL_ID,
			@P_EMPRESA_ID,
			@P_ESPACIOS_PARQUEO
          ) 	
	End
GO  