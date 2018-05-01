CREATE PROCEDURE [dbo].[CRE_RUTA_PR]
    @P_RUTA_DESCRIPCION nvarchar(100),
    @P_JSON_ROUTE nvarchar(max),
    @P_LATITUDE_DESTINO decimal(20,17),
    @P_LONGITUDE_DESTINO decimal(20,17),
    @P_DISTANCIA decimal(10,3),
    @P_LINEA_ID int,
    @P_TERMINAL_ID int,
    @P_TARIFA_ID int,
    @P_ESTADO NVARCHAR(10)
AS
	INSERT INTO [dbo].[TBL_RUTA]
           ([RUTA_DESCRIPCION]
           ,[JSON_ROUTE]
           ,[LATITUDE_DESTINO]
           ,[LONGITUDE_DESTINO]
           ,[DISTANCIA]
           ,[LINEA_ID]
           ,[TERMINAL_ID]
           ,[TARIFA_ID]
           ,[ESTADO])
     VALUES
           (@P_RUTA_DESCRIPCION,
           @P_JSON_ROUTE,
           @P_LATITUDE_DESTINO,
           @P_LONGITUDE_DESTINO,
           @P_DISTANCIA,
           @P_LINEA_ID,
           @P_TERMINAL_ID,
           @P_TARIFA_ID,
           @P_ESTADO)
	SELECT SCOPE_IDENTITY() AS RUTA_ID
RETURN 0
