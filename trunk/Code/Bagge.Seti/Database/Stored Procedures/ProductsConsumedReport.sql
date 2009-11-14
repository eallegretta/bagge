

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductsConsumedReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProductsConsumedReport]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* SELECCIONAR TODOS LOS INSUMOS Y CANTIDAD QUE SE CONSUMIERON 
DURANTE TODO EL MES (O DESDE HASTA) ORDEN DESCENDENTE */

CREATE procedure [dbo].[ProductsConsumedReport]

@dateFrom datetime,
@dateTo datetime,
@productName varchar(50),
@estimatedQuantityFrom int,
@estimatedQuantityTo int

as

select	P.Name 'Nombre',
		P.Description 'Descripción',
		sum(PT.EstimatedQuantity) 'Total Cantidad Estimada'		

from  ProductTicket PT 
			inner join ProductProvider PP on PT.ProductProviderId = PP.Id
			inner join Product P on PP.ProductId = P.Id
			inner join Ticket T on PT.TicketId = T.Id 
where	P.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId= 6 and 
		T.ExecutionDateTime between @dateFrom and @dateTo and
		P.Name like @productName
		
GROUP BY	P.Name,
			P.Description

having	sum(PT.EstimatedQuantity) between @estimatedQuantityFrom and @estimatedQuantityTo

order by sum(PT.EstimatedQuantity) desc


