
/****** Object:  StoredProcedure [dbo].[TechniciansByTicketReport]    Script Date: 05/25/2009 20:28:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketHistoryReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TicketHistoryReport]
GO
/****** Object:  StoredProcedure [dbo].[TechniciansByTicketReport]    Script Date: 05/25/2009 20:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* OBTENER EL HISTORIAL DE UN TICKET, PERMITE FILTRO POR ID DE TICKET, FECHA Y ESTADO */

CREATE procedure [dbo].[TicketHistoryReport]
@ticketIdFrom int,
@ticketIdTo int,
@ticketStatusIdFrom int,
@ticketStatusIdTo int,
@dateFrom datetime,
@dateTo datetime

as

select	
		h.TicketId 'Id de Ticket',
		h.DateTime 'Fecha y Hora',
		s.Name 'Estado',
		e.Lastname + ', ' + e.Firstname 'Empleado',
		h.Notes 'Observaciones'
from  
		TicketHistory h
				inner join TicketStatus s on h.TicketStatusId = s.Id
				inner join Employee e on h.EmployeeId = e.Id
where	h.TicketId between @ticketIdFrom and @ticketIdTo and
		h.TicketStatusId between @ticketStatusIdFrom and @ticketStatusIdTo and
		h.DateTime between @dateFrom and @dateTo
order by h.TicketId, h.DateTime desc, e.Lastname, e.Firstname

