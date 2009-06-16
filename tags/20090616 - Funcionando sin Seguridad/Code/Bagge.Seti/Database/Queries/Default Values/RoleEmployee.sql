declare @superAdminRoleId int 
set @superAdminRoleId = (select Id from Role where Id = 1)

insert into RoleEmployee (RoleId, EmployeeId)
select top 1 @superAdminRoleId, Id from Employee
 