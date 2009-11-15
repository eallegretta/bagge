IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketsClosedReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TicketsClosedReport]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*SELECCIONAR LOS TICKETS CERRADOS EN UN RANGO DE FECHAS Y MOSTRAR ADMINISTRATIVO (CREADOR DEL TICKET), 
TÉCNICO, CANTIDAD DE TICKETS, CANTIDAD DE HORAS PRESUPUESTADAS, CANTIDAD DE HORAS REALES, PORCENTAJE DE DESVÍO.
PERMITIR AGRUPAR POR TECNICO O POR ADMINISTRATIVO 

EL DESVÍO SE CALCULA DE LA SIGUIENTE MANERA EN TODAS LAS LINEAS, INCLUSO TOTALES:
	SI HORAS PRESUPUESTADAS >< 0:
	DESVÍO = (HORAS REALES – HORAS PRESUPUESTADAS) * 100  / HORAS PRESUPUESTADAS
	SI HORAS PRESUPUESTADAS = 0:
	DESVÍO = 0%
*/

CREATE procedure [dbo].[TicketsClosedReport]
@dateFrom datetime,
@dateTo datetime,
@groupByCreator bit,
@technicianName varchar(50),
@adminName varchar(50)

as

create table #tickets (
	Admin varchar(100),
	Technician varchar(100),
	Tickets int,
	EstimatedDuration decimal(8,2),
	RealDuration decimal(8,2),
	Detour decimal(8,2) )


if @groupByCreator = 1
begin
	insert into #tickets
	select	C.Lastname + ', ' + C.Firstname,
			E.Lastname + ', ' + E.Firstname,
			count(T.Id),
			sum(EstimatedDuration),
			sum(RealDuration),
			case 
				when sum(EstimatedDuration) > 0 then (((sum(RealDuration) - sum(EstimatedDuration)) *  100) / sum(EstimatedDuration))
				when sum(EstimatedDuration) = 0 then 0
			end
	from	Ticket T 
				inner join Employee C on T.EmployeeCreatorId = C.Id
				inner join TicketEmployee TE on T.Id = TE.TicketId
				inner join Employee E on TE.EmployeeId = E.Id
	where	
			T.Deleted = 0 and
			C.Deleted = 0 and
			E.Deleted = 0 and
			T.TicketStatusId = 6 and
			T.ExecutionDateTime between @dateFrom and @dateTo

			and

			(
				(
					((C.Lastname + ', ' + C.Firstname) like   @adminName ) 
				)			and 
				(
					((E.Lastname + ', ' + E.Firstname) like   @technicianName)
				)	
			)

	group by	C.Lastname + ', ' + C.Firstname, E.Lastname + ', ' + E.Firstname
	order by 	C.Lastname + ', ' + C.Firstname, E.Lastname + ', ' + E.Firstname
	
	select 
		Admin 'Administrativo',
		Technician 'Técnico',
		Tickets,
		EstimatedDuration 'Duración Estimada',
		RealDuration 'Duración Real',
		Detour 'Desvio'	
	from #tickets
	union
	select	Admin + ' - Total',
			'',
			sum(Tickets),
			sum(EstimatedDuration),
			sum(RealDuration),
			case 
				when sum(EstimatedDuration) > 0 then (((sum(RealDuration) - sum(EstimatedDuration)) *  100) / sum(EstimatedDuration))
				when sum(EstimatedDuration) = 0 then 0
			end
	from #tickets
	group by	Admin
	order by	Admin
end
else
begin
insert into #tickets
	select	C.Lastname + ', ' + C.Firstname,
			E.Lastname + ', ' + E.Firstname,
			count(T.Id),
			sum(EstimatedDuration),
			sum(RealDuration),
			case 
				when sum(EstimatedDuration) > 0 then (((sum(RealDuration) - sum(EstimatedDuration)) *  100) / sum(EstimatedDuration))
				when sum(EstimatedDuration) = 0 then 0
			end
	from	Ticket T 
				inner join Employee C on T.EmployeeCreatorId = C.Id
				inner join TicketEmployee TE on T.Id = TE.TicketId
				inner join Employee E on TE.EmployeeId = E.Id
	where	
			T.Deleted = 0 and
			C.Deleted = 0 and
			E.Deleted = 0 and
			T.TicketStatusId = 6 and
			T.ExecutionDateTime between @dateFrom and @dateTo

			and

			(
				(
					((C.Lastname + ', ' + C.Firstname) like  @adminName ) 
				)			and 
				(
					((E.Lastname + ', ' + E.Firstname) like  @technicianName )
				)	
			)

	group by	E.Lastname + ', ' + E.Firstname, C.Lastname + ', ' + C.Firstname
	order by 	E.Lastname + ', ' + E.Firstname, C.Lastname + ', ' + C.Firstname
	
	select 
		Technician 'Técnico',
		Admin 'Administrativo',
		Tickets,
		EstimatedDuration 'Duración Estimada',
		RealDuration 'Duración Real',
		Detour 'Desvio'	
	from #tickets
	union
	select	Technician + ' - Total',
			'',
			sum(Tickets),
			sum(EstimatedDuration),
			sum(RealDuration),
			case 
				when sum(EstimatedDuration) > 0 then (((sum(RealDuration) - sum(EstimatedDuration)) *  100) / sum(EstimatedDuration))
				when sum(EstimatedDuration) = 0 then 0
			end
	from #tickets
	group by	Technician
	order by	Technician
end
drop table #tickets
	
	
