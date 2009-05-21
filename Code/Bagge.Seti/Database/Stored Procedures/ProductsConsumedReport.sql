IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ProductsConsumedReport')
	BEGIN
		DROP  Procedure  ProductsConsumedReport
	END

GO

create procedure ProductsConsumedReport
@dateFrom datetime,
@dateTo datetime
as

select	
		P.Name,
		P.Description,
		sum(PT.EstimatedQuantity)		

from  ProductTicket PT inner join Product P on  PT.ProductId = P.Id
						inner join Ticket T on PT.TicketId = T.Id 
where	P.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId= 6 --cerrado
		and 
		T.ExecutionDate between @dateFrom and @dateTo
		
--select * from dbo.TicketStatus

GROUP BY	P.Id,
			P.Name,
			P.Description

order by P.Name desc

