IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TechniciansByTicketReport')
	BEGIN
		DROP  Procedure  TechniciansByTicketReport
	END

GO

create procedure TechniciansByTicketReport
@dateFrom datetime,
@dateTo datetime
as

select	E.Firstname,
		E.Lastname,
		count(TE.Id) 'TicketsCount', --cantidad tickets	
		sum(T.RealDuration) --horas consumidas desde hasta

from  dbo.TicketEmployee TE inner join Employee E on TE.EmployeeId = E.Id
							inner join Ticket T on TE.TicketId = T.Id
						
where	E.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId= 6 --cerrado
		and 
		T.ExecutionDate between @dateFrom and @dateTo

--select * from dbo.TicketStatus

GROUP BY	E.Id,
			E.Firstname,
			E.Lastname

