CREATE PROCEDURE [dbo].[RET_ALL_TRANSACTIONS_BY_USER]
	@P_EMAIL nvarchar(50)
AS
	Select tr.*, t.USUARIO as EMAIL from TBL_TRANSACCION as tr
	join TBL_TARJETA  t on t.CODIGO_UNICO = tr.TARJETA_ID
	where tr.TARJETA_ID in(
						select ta.CODIGO_UNICO	
						from TBL_TARJETA as ta
						where ta.USUARIO = @P_EMAIL)
RETURN 0
