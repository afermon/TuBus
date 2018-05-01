CREATE PROCEDURE [dbo].[CRE_TERMINAL_PR]
	@P_terminal_name NVARCHAR(50),
	@P_latitude DECIMAL(18,16),
	@P_longitude DECIMAL(18,16),
	@P_cantidad_lineas INT,
	@P_estado NVARCHAR(10),
	@P_espacios_parqueo int,
	@P_espacios_parqueo_bus int
AS
	INSERT INTO [dbo].[TBL_TERMINAL] VALUES(@P_terminal_name,@P_latitude,@P_longitude,@P_cantidad_lineas,@P_estado, @P_espacios_parqueo, @P_espacios_parqueo_bus);

RETURN 0