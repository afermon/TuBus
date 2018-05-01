﻿CREATE PROCEDURE [dbo].[UPD_ROLE_PR]
	@P_ROLE_ID nvarchar(50),
    @P_DESCRIPCION nvarchar(100),
    @P_HOMEPAGE nvarchar(100)
AS
	UPDATE [dbo].[TBL_ROLE]
	SET [DESCRIPCION] = @P_DESCRIPCION
      ,[HOMEPAGE] = @P_HOMEPAGE
	WHERE ROLE_ID = @P_ROLE_ID
RETURN 0