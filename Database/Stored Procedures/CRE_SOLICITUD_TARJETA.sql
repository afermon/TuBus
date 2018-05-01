CREATE PROCEDURE [dbo].[CRE_SOLICITUD_TARJETA]
	@P_ID_SOLICITUD  nvarchar(50),
	@P_TERMINAL_ID int,
    @P_CEDULA_JURIDICA int,
	@P_EMAILS nvarchar(MAX),
	@P_EMAIL_SOLICITANTE nvarchar(50)
AS 
	Begin
		SET NOCOUNT ON;  
		INSERT INTO TBL_SOLICITUDES
          ( 
			ID_SOLICITUD,
			TERMINAL_ID,
			CEDULA_JURIDICA,
			EMAILS,
			EMAIL_SOLICITANTE,
			ESTADO			
          ) 
     VALUES 
          ( 
		    @P_ID_SOLICITUD,
		    @P_TERMINAL_ID,
            @P_CEDULA_JURIDICA,
			@P_EMAILS,
			@P_EMAIL_SOLICITANTE,
			'Pendiente'
          ) 	
	End
GO  

