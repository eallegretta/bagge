USE [SETI]
GO
/****** Object:  StoredProcedure [dbo].[CustomersWithPendingPaymentReport]    Script Date: 05/25/2009 20:25:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomersWithPendingPaymentReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CustomersWithPendingPaymentReport]
GO
/****** Object:  StoredProcedure [dbo].[CustomersWithPendingPaymentReport]    Script Date: 05/25/2009 20:25:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[CustomersWithPendingPaymentReport]
as

select  C.Name 'Nombre',
		C.CUIT 'Cuit',
		C.Address 'Direccion',
		C.Floor 'Psio',
		C.Departament 'Deptartamento',
		C.ZipCode 'Codigo_Postal',
		C.City 'Ciudad',
		C.Phone 'Telefono',
		C.MobilePhone 'Celular',
		T.Budget 'Presupuesto',
		C.Email 'EMail',
		C.Subscription 'Abonado'




from Customer C inner join Ticket T on T.CustomerId = C.Id     

where	C.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId = 4

order by T.Budget desc

GO
