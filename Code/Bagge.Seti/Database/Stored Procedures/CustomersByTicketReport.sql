USE [SETI]
GO
/****** Object:  StoredProcedure [dbo].[CustomersByTicketReport]    Script Date: 05/23/2009 20:16:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomersByTicketReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CustomersByTicketReport]
GO
/****** Object:  StoredProcedure [dbo].[CustomersByTicketReport]    Script Date: 05/23/2009 20:16:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomersByTicketReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[CustomersByTicketReport]
@dateFrom datetime,
@dateTo datetime
as

select 		C.Name,
			C.CUIT,
			count(T.Id) ''TicketsCount'',
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
			C.CUIT' 
END
GO
