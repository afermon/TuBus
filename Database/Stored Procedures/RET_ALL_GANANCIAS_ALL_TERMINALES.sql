CREATE PROCEDURE [dbo].[RET_ALL_GANANCIAS_ALL_TERMINALES]
AS
	Select Sum(r.Ganancias) as GANANCIAS, r.FECHA, ter.TERMINAL_NAME  as NOMBRE from (
		select Sum(t.Ganacias) as Ganancias, t.fecha, t.TERMINAL_ID from (
			Select Sum(tr.MONTO*0.1) as Ganacias, CONVERT(char(10), tr.Fecha,126) as fecha, tr.TERMINAL_ID from TBL_TRANSACCION as tr
				where tr.TARJETA_ID is not null
				group by tr.TERMINAL_ID, fecha) as t	
		group by t.TERMINAL_ID, t.fecha
		union 
		select Sum(t.Ganacias) as GANANCIAS, t.FECHA, t.TERMINAL_ID  as NOMBRE from (
			Select Sum(tr.MONTO*0.1) as Ganacias, CONVERT(char(10), tr.Fecha,126) as fecha, tr.TERMINAL_ID from TBL_TRANSACCION as tr
				where tr.TARJETA_ID is null
				group by tr.TERMINAL_ID, fecha) as t
		group by t.TERMINAL_ID, t.fecha) as r
		join TBL_TERMINAL as ter
		on ter.TERMINAL_ID = r.TERMINAL_ID
	group by ter.TERMINAL_NAME, r.fecha
RETURN 0
