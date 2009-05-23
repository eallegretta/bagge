USE [SETI]
GO
/****** Object:  StoredProcedure [dbo].[CustomersWithPendingPaymentReport]    Script Date: 05/23/2009 20:19:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomersWithPendingPaymentReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CustomersWithPendingPaymentReport]
GO
/****** Object:  StoredProcedure [dbo].[CustomersWithPendingPaymentReport]    Script Date: 05/23/2009 20:19:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomersWithPendingPaymentReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
create procedure [dbo].[CustomersWithPendingPaymentReport]
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

' 
END
GO
