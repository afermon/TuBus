CREATE PROCEDURE [dbo].[CRE_HISTORIAL_CONTRASENA_PR]
	@P_PASSWORD_SET datetime,
    @P_EMAIL nvarchar(50),
    @P_PASSWORD_HASH nvarchar(24)
AS
	INSERT INTO [dbo].[TBL_HISTORIAL_PASSWORD]
           ([PASSWORD_SET]
           ,[EMAIL]
           ,[PASSWORD_HASH])
     VALUES
           (@P_PASSWORD_SET
           ,@P_EMAIL
           ,@P_PASSWORD_HASH)
RETURN 0
