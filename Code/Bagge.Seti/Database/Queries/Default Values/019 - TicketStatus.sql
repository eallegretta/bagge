 set identity_insert TicketStatus on
 
 insert into TicketStatus (Id, Name)
 select 8, 'Cancelado por cliente'
 union
 select 9, 'Cancelado por sistema'
 
 set identity_insert TicketStatus off 