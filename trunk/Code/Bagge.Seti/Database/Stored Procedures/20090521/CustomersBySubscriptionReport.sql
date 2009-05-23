IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CustomersBySubscriptionReport')
	BEGIN
		DROP  Procedure  CustomersBySubscriptionReport
	END

GO

create procedure CustomersBySubscriptionReport
@valueFrom decimal,
@valueTo decimal
as

	select	C.Name,
			C.CUIT,
			C.Address,
			C.Floor,
			C.Departament,
			C.ZipCode,
			C.City,
			C.Phone,
			C.MobilePhone,
			C.Email,
			T.Budget
	
	from Customer C inner join Ticket T on T.CustomerId = C.Id     

	where	C.Deleted = 0 and
			T.Deleted = 0 and
			C.Subscription = 1 and
			T.Budget between @valueFrom and @valueTo

	order by T.Budget asc
