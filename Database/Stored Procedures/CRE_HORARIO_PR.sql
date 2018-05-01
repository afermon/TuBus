CREATE PROCEDURE [dbo].[CRE_HORARIO_PR]
	@P_RUTA_ID int
    ,@P_DIA_SEMANA int
    ,@P_HORA_SALIDA time(0)
AS
	INSERT INTO [dbo].[TBL_HORARIO]
           ([RUTA_ID]
           ,[DIA_SEMANA]
           ,[HORA_SALIDA])
     VALUES
           (@P_RUTA_ID
           ,@P_DIA_SEMANA
           ,@P_HORA_SALIDA)
RETURN 0
