CREATE PROCEDURE [dbo].[CRE_TRANSACCION_PR]
	 @P_MONTO int
    ,@P_TIPO nvarchar(45)
    ,@P_CONCEPTO nvarchar(45)
    ,@P_ESTADO nvarchar(45)
    ,@P_TARJETA_ID nvarchar(50) = null
    ,@P_LINEA_ID int = null
	,@P_TERMINAL_ID int = null
AS
	INSERT INTO [dbo].[TBL_TRANSACCION]
           ([MONTO]
           ,[TIPO]
           ,[CONCEPTO]
           ,[ESTADO]
           ,[TARJETA_ID]
           ,[LINEA_ID]
		   ,[TERMINAL_ID]
		   ,[FECHA])
     VALUES
           ( @P_MONTO 
			,@P_TIPO 
			,@P_CONCEPTO 
			,@P_ESTADO 
			,@P_TARJETA_ID 
			,@P_LINEA_ID
			,@P_TERMINAL_ID
			, GETDATE())
RETURN 0
