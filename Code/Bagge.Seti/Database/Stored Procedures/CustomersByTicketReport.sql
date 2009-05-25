USE [SETI]
GO
/****** Object:  StoredProcedure [dbo].[CustomersByTicketReport]    Script Date: 05/25/2009 20:23:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomersByTicketReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CustomersByTicketReport]
GO
/****** Object:  StoredProcedure [dbo].[CustomersByTicketReport]    Script Date: 05/25/2009 20:23:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[CustomersByTicketReport]

@dateFrom datetime,
@dateTo datetime

as

select 		C.Name	'Nombre',
			C.CUIT	'Cuit',
			count(T.Id) 'Cantidad de Tickets',
			sum(T.RealDuration)	'Sum_Duracion Real',
			sum(T.Budget) 'Sum_Presupuesto',
			C.Subscription 'Abonado'

from  Customer C inner join Ticket T on  C.Id = T.CustomerId
							
where	C.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId= 6
		and T.ExecutionDate between @dateFrom and @dateTo

group by    C.Name,
			C.CUIT,
			C.Subscription

order by	C.Name,
			C.CUIT

