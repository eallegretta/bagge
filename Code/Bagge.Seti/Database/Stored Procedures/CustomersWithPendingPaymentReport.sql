IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CustomersWithPendingPaymentReport')
	BEGIN
		DROP  Procedure  CustomersWithPendingPaymentReport
	END

GO

create procedure CustomersWithPendingPaymentReport
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
		T.Budget,
		C.Subscription
from Customer C inner join Ticket T on T.CustomerId = C.Id     

where	C.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId = 4

order by T.Budget desc

