 set identity_insert TicketStatus on
 
 insert into TicketStatus (Id, Name)
 select 1, 'Inicial'
 union
 select 2, 'Abierto'
 union
 select 3, 'Pendiente de aprobación'
 union
 select 4, 'Pendiente de pago'
 union
 select 5, 'Vencido'
 union
 select 6, 'Cerrado'
 union 
 select 7, 'Cancelado'
 
 set identity_insert TicketStatus off
