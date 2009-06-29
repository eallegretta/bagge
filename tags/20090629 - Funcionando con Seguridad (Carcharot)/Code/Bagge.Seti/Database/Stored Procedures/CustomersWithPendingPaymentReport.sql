
/****** Object:  StoredProcedure [dbo].[CustomersWithPendingPaymentReport]    Script Date: 05/25/2009 20:25:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomersWithPendingPaymentReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CustomersWithPendingPaymentReport]
GO
/****** Object:  StoredProcedure [dbo].[CustomersWithPendingPaymentReport]    Script Date: 05/25/2009 20:25:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* SELECCIONAR TODOS LOS CLIENTES QUE ESTAN PENDIENTE DE PAGO 
ORDENAR POR LO QUE DEBE Y DESCENDENTE, ESPECIFICANDO SI ES C/ABONO O  S/ABONO */

CREATE procedure [dbo].[CustomersWithPendingPaymentReport]
as

select  C.Name 'Nombre',
		C.CUIT 'CUIT',
		C.Address 'Dirección',
		C.Floor 'Piso',
		C.Departament 'Deptartamento',
		C.ZipCode 'Código Postal',
		C.City 'Ciudad',
		C.Phone 'Teléfono',
		C.MobilePhone 'Celular',
		T.Budget 'Presupuesto',
		C.Email 'E-Mail',
		C.Subscription 'Abonado'




from Customer C inner join Ticket T on T.CustomerId = C.Id     

where	C.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId = 4

order by T.Budget desc

GO
