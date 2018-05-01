CREATE PROCEDURE [dbo].[CRE_PAGOPENDIENTE_PR]
	 @P_MONTO int
    ,@P_LINEA_ID int 
	,@P_CEDULA_JURIDICA int 
AS
	INSERT INTO [dbo].[TBL_PAGOS_PENDIENTES]
           (LINEA_ID
		   ,[MONTO]
		   ,ESTADO_PAGO
           ,CEDULA_JURIDICA
		   ,FECHA
		   ,DESCRIPCION)
     VALUES
           (@P_LINEA_ID
		    ,@P_MONTO 
			, 'Pendiente'
			,@P_CEDULA_JURIDICA 
			, GETDATE()
			,'Cobro parqueo buses')
RETURN 0
