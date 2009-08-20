alter table Ticket drop column CustomerETA
go
sp_rename 'Ticket.ExecutionDate' , 'ExecutionDateTime', 'COLUMN'
go

alter table Ticket add Notes varchar(max) NULL
go
