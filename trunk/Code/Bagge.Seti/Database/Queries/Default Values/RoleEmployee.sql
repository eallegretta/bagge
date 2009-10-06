declare @superAdminRoleId int 
set @superAdminRoleId = (select Id from Role where Id = 1)

print @superAdminRoleId

insert into RoleEmployee (1, EmployeeId)
select top 1 @superAdminRoleId, Id from Employee
 select * from select * from RoleEmployee
select * from Employee
select * from Role

sp_columns  RoleEmployee

insert into RoleEmployee

(
RoleId,
EmployeeId
)
values
(
1,
10
)

RoleId
EmployeeId