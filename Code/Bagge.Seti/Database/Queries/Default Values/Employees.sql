
insert into Employee (Username, Password, Lastname, Firstname, Email, CategoryId, AuditUserName)
select 'test','098f6bcd4621d373cade4e832627b4f6','test','test','baggeseti@gmail.com', Id, 'install' from EmployeeCategory where Name = 'Administrador'
