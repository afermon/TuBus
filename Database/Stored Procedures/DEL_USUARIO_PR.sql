CREATE PROCEDURE [dbo].[DEL_USUARIO_PR]
	@P_EMAIL nvarchar(50)
AS
	Update [dbo].[TBL_USUARIO]
	set
		ESTADO = 'Inactivo'
    WHERE EMAIL = @P_EMAIL
RETURN 0
