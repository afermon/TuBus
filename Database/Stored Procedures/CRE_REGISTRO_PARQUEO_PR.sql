CREATE PROCEDURE [dbo].[CRE_REGISTRO_PARQUEO_PR]
	@P_TERMINAL_ID INT,
	@P_HORA_INICIO datetime,
	@P_TARJETA_ID nvarchar(50)

	AS

INSERT INTO [dbo].[TBL_REGISTROPARQUEO]
           ([TERMINAL_ID]
           ,[HORA_INICIO]
           ,[TARJETA_ID])
     VALUES
           (@P_TERMINAL_ID
           ,@P_HORA_INICIO
           ,@P_TARJETA_ID)
GO