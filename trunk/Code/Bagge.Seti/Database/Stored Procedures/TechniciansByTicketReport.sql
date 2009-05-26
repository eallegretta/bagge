
/****** Object:  StoredProcedure [dbo].[TechniciansByTicketReport]    Script Date: 05/25/2009 20:28:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TechniciansByTicketReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TechniciansByTicketReport]
GO
/****** Object:  StoredProcedure [dbo].[TechniciansByTicketReport]    Script Date: 05/25/2009 20:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* SELECCIONAR TODOS LOS TECNICOS DE ACUERDO A LA CANTIDAD DE TICKETS QUE EJECUTARON 
Y HORAS CONSUMIDAS DURANTE TODO EL MES (O DESDE HASTA) */

CREATE procedure [dbo].[TechniciansByTicketReport]
@dateFrom datetime,
@dateTo datetime
as

select	E.Firstname 'Nombre',
		E.Lastname 'Apellido',
		count(TE.Id) 'Cantidad de Tickets',
		sum(T.RealDuration) 'Total Duracion Real'

from  dbo.TicketEmployee TE inner join Employee E on TE.EmployeeId = E.Id
							inner join Ticket T on TE.TicketId = T.Id
						
where	E.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId= 6 and 
		T.ExecutionDate between @dateFrom and @dateTo

GROUP BY	E.Id,
			E.Firstname,
			E.Lastname


