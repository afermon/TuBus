CREATE PROCEDURE [dbo].[CRE_ROLE_PR]
	@P_ROLE_ID nvarchar(50),
    @P_DESCRIPCION nvarchar(100),
    @P_HOMEPAGE nvarchar(100)
AS
	INSERT INTO [dbo].[TBL_ROLE]
           ([ROLE_ID]
           ,[DESCRIPCION]
           ,[HOMEPAGE]
           ,[ESTADO])
     VALUES
           (@P_ROLE_ID
           ,@P_DESCRIPCION
           ,@P_HOMEPAGE
           ,'Activo')
RETURN 0
