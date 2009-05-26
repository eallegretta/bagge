USE [SETI]
GO
/****** Object:  StoredProcedure [dbo].[ProductsConsumedReport]    Script Date: 05/25/2009 20:27:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductsConsumedReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProductsConsumedReport]
GO
/****** Object:  StoredProcedure [dbo].[ProductsConsumedReport]    Script Date: 05/25/2009 20:27:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* SELECCIONAR TODOS LOS INSUMOS Y CANTIDAD QUE SE CONSUMIERON 
DURANTE TODO EL MES (O DESDE HASTA) ORDEN DESCENDENTE */

CREATE procedure [dbo].[ProductsConsumedReport]

@dateFrom datetime,
@dateTo datetime

as

select	P.Name 'Nombre',
		P.Description 'Descripción',
		sum(PT.EstimatedQuantity) 'Total Cantidad Estimada'		

from  ProductTicket PT inner join Product P on  PT.ProductId = P.Id
						inner join Ticket T on PT.TicketId = T.Id 
where	P.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId= 6 and 
		T.ExecutionDate between @dateFrom and @dateTo
		
GROUP BY	P.Name,
			P.Description

order by sum(PT.EstimatedQuantity) desc


