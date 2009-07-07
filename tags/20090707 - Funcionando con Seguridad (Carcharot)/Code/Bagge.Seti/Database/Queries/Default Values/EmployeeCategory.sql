delete EmployeeCategory

insert into EmployeeCategory (Name, Description) 
select 'Administrativo', ''
union
select 'Secretaria',''
union
select 'Técnico',''
union
select 'Administrador',''