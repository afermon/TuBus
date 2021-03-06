﻿CREATE PROCEDURE [dbo].[RET_GANACIAS_ALL_TIPOS_TARJETAS]
AS
	Select count(tr.TARJETA_ID) as CANTIDAD, tp.NOMBRE_TARJETA as NOMBRE from TBL_TRANSACCION as tr
		join TBL_TARJETA as t 
		on t.CODIGO_UNICO = tr.TARJETA_ID 
		join TBL_TIPOTARJETA as tp 
		on tp.TIPOTARJETA_ID = t.TIPOTARJETA_ID
		where t.CONVENIO_ID is null
	group by tp.NOMBRE_TARJETA

union 

	Select count(tr.TARJETA_ID) as CANTIDAD, tc.NOMBRE_INSTITUCION as NOMBRE from TBL_TRANSACCION as tr
		join TBL_TARJETA as t 
		on t.CODIGO_UNICO = tr.TARJETA_ID 
		join TBL_CONVENIO as tc 
		on tc.CEDULA_JURIDICA = t.CONVENIO_ID
		where t.CONVENIO_ID is not null
	group by tc.NOMBRE_INSTITUCION
RETURN 0
