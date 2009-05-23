USE [SETI]
GO
/****** Object:  StoredProcedure [dbo].[TechniciansByTicketReport]    Script Date: 05/23/2009 20:23:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TechniciansByTicketReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TechniciansByTicketReport]
GO
/****** Object:  StoredProcedure [dbo].[TechniciansByTicketReport]    Script Date: 05/23/2009 20:23:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TechniciansByTicketReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[TechniciansByTicketReport]
@dateFrom datetime,
@dateTo datetime
as

select	E.Firstname,
		E.Lastname,
		count(TE.Id) ''TicketsCount'',
		sum(T.RealDuration)

from  dbo.TicketEmployee TE inner join Employee E on TE.EmployeeId = E.Id
							inner join Ticket T on TE.TicketId = T.Id
						
where	E.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId= 6 and 
		T.ExecutionDate between @dateFrom and @dateTo

GROUP BY	E.Id,
			E.Firstname,
			E.Lastname

' 
END
GO
