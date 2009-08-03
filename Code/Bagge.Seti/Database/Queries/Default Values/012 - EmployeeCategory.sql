begin tran

declare @administratorId int, @secretaryId int, @technicianId int, @administrativeId int

set @administratorId = (select Id from EmployeeCategory where Name = 'Administrador')
set @secretaryId = (select Id from EmployeeCategory where Name = 'Secretaria')
set @administrativeId = (select Id from EmployeeCategory where Name = 'Administrativo')
set @technicianId = (select Id from EmployeeCategory where Name = 'Técnico')


update	Employee 
set		CategoryId = @administrativeId 
where	CategoryId in (@administratorId, @secretaryId)


delete from EmployeeCategory where Id in (@administratorId, @secretaryId)

if @@error <> 0
	rollback tran
else
	commit tran