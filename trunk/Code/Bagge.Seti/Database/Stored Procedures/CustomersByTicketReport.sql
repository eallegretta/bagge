
/****** Object:  StoredProcedure [dbo].[CustomersByTicketReport]    Script Date: 05/25/2009 20:23:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomersByTicketReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CustomersByTicketReport]
GO
/****** Object:  StoredProcedure [dbo].[CustomersByTicketReport]    Script Date: 05/25/2009 20:23:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* SELECCIONAR TODOS LOS CLIENTES POR CANTIDAD DE TICKETS EJECUTADOS (CERRADO), 
HORAS CONSUMIDAS Y “BUDGET” C/ABONO Y  S/ABONO, QUE SE EJECUTARON DURANTE 
TODO EL MES (O DESDE HASTA). ORDEN ASCENDENTE POR CLIENTE */
 
CREATE procedure [dbo].[CustomersByTicketReport]

@dateFrom datetime=null,
@dateTo datetime=null,
@customerName varchar(50),
@ticketCountFrom int,
@ticketCountTo int,
@realDurationFrom decimal,
@realDurationTo decimal,
@budgetFrom decimal,
@budgetTo decimal


as
begin
select 		C.Name	'Nombre',
				C.CUIT	'CUIT',
				count(T.Id) 'Cantidad de Tickets',
				sum(T.RealDuration)	'Total Duración Real',
				sum(T.Budget) 'Total Presupuestos',
				C.Subscription 'Abonado'

	from  Customer C inner join Ticket T on  C.Id = T.CustomerId
								
	where	C.Deleted = 0 and
			T.Deleted = 0 and
			T.TicketStatusId= 6 and 
			T.ExecutionDateTime between @dateFrom and @dateTo and
			C.Name like @customerName
	
	group by    C.Name,
				C.CUIT,
				C.Subscription
	
	having	
			count(T.Id) between @ticketCountFrom and @ticketCountTo and
			sum(T.RealDuration) between @realDurationFrom and @realDurationTo and
			sum(T.Budget) between @budgetFrom and @budgetTo 


	order by	C.Name,
				C.CUIT
end



