﻿CREATE PROCEDURE [dbo].[RET_CONFIGURACION_PR]
	@P_CONFIG_ID nvarchar(50)
AS
	SELECT * FROM [TBL_CONFIGURACION]
	WHERE [CONFIG_ID] = @P_CONFIG_ID
RETURN 0