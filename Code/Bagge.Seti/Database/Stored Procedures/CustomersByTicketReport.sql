IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CustomersByTicketReport')
	BEGIN
		DROP  Procedure  CustomersByTicketReport
	END

GO

create procedure CustomersByTicketReport
@dateFrom datetime,
@dateTo datetime
as

select 		C.Name,
			C.CUIT,
			count(T.Id) 'TicketsCount',
			sum(T.RealDuration),
			sum(T.Budget),
			C.Subscription

from  Customer C inner join Ticket T on  C.Id = T.CustomerId
							
where	C.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId= 6
		and T.ExecutionDate between @dateFrom and @dateTo

group by    C.Name,
			C.CUIT,
			C.Subscription

order by	C.Name,
			C.CUIT,
			C.Subscription 