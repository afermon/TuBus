﻿CREATE PROCEDURE [dbo].[UPD_PAGOPENDIENTE_PR]
    @P_ID_PAGO int
AS 
	Begin
		SET NOCOUNT ON;  
		UPDATE  TBL_PAGOS_PENDIENTES
		SET           
		   ESTADO_PAGO = 'Cancelado'
		   where ID_PAGO = @P_ID_PAGO
	End
GO  
