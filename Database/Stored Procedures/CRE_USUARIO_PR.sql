CREATE PROCEDURE [dbo].[CRE_USUARIO_PR]
	@P_EMAIL nvarchar(50)
    ,@P_PASSWORD_SALT nvarchar(24)
    ,@P_PASSWORD_HASH nvarchar(24)
    ,@P_PASSWORD_LAST_SET datetime
    ,@P_IDENTIFICACION nvarchar(50)
    ,@P_NOMBRE nvarchar(50)
    ,@P_APELLIDOS nvarchar(50)
    ,@P_FECHA_NACIMIENTO date
    ,@P_ESTADO nvarchar(50)
    ,@P_ROLE_ID nvarchar(50)
	,@P_NOTIFY_USER_AT int
	,@P_PHONE int
	,@P_TERMINAL_ID int = null
AS
	INSERT INTO [dbo].[TBL_USUARIO]
           ([EMAIL]
           ,[PASSWORD_SALT]
           ,[PASSWORD_HASH]
           ,[PASSWORD_LAST_SET]
           ,[IDENTIFICACION]
           ,[NOMBRE]
           ,[APELLIDOS]
           ,[FECHA_NACIMIENTO]
           ,[ESTADO]
           ,[ROLE_ID]
		   ,[NOTIFY_USER_AT]
		   ,[PHONE]
		   ,[TERMINAL_ID])
     VALUES
           (@P_EMAIL
			,@P_PASSWORD_SALT
			,@P_PASSWORD_HASH
			,@P_PASSWORD_LAST_SET
			,@P_IDENTIFICACION
			,@P_NOMBRE
			,@P_APELLIDOS
			,@P_FECHA_NACIMIENTO
			,@P_ESTADO
			,@P_ROLE_ID
			,@P_NOTIFY_USER_AT
			,@P_PHONE
			,@P_TERMINAL_ID)
RETURN 0
