
delete RoleFunction
delete [Function]

insert into [Function] (Name, FullQualifiedName, Action) values ('Proveedores - Listar', 'Bagge.Seti.WebSite.ProviderList', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Proveedores - Borrar', 'Bagge.Seti.WebSite.ProviderList', 'D')
insert into [Function] (Name, FullQualifiedName, Action) values ('Proveedores - Crear', 'Bagge.Seti.WebSite.ProviderEditor', 'C')
insert into [Function] (Name, FullQualifiedName, Action) values ('Proveedores - Editar', 'Bagge.Seti.WebSite.ProviderEditor', 'U')
insert into [Function] (Name, FullQualifiedName, Action) values ('Proveedores - Ver', 'Bagge.Seti.WebSite.ProviderEditor', 'R')

insert into [Function] (Name, FullQualifiedName, Action) values ('Productos - Listar', 'Bagge.Seti.WebSite.ProductList', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Productos - Borrar', 'Bagge.Seti.WebSite.ProductList', 'D')
insert into [Function] (Name, FullQualifiedName, Action) values ('Productos - Crear', 'Bagge.Seti.WebSite.ProductEditor', 'C')
insert into [Function] (Name, FullQualifiedName, Action) values ('Productos - Editar', 'Bagge.Seti.WebSite.ProductEditor', 'U')
insert into [Function] (Name, FullQualifiedName, Action) values ('Productos - Ver', 'Bagge.Seti.WebSite.ProductEditor', 'R')

insert into [Function] (Name, FullQualifiedName, Action) values ('Inicio', 'Bagge.Seti.WebSite._Default', 'R')

insert into [Function] (Name, FullQualifiedName, Action) values ('Alertas - Configurar', 'Bagge.Seti.WebSite.AlertConfigurationEditor', 'U')

insert into [Function] (Name, FullQualifiedName, Action) values ('Empleados - Listar', 'Bagge.Seti.WebSite.EmployeeList', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Empleados - Borrar', 'Bagge.Seti.WebSite.EmployeeList', 'D')
insert into [Function] (Name, FullQualifiedName, Action) values ('Empleados - Crear', 'Bagge.Seti.WebSite.EmployeeEditor', 'C')
insert into [Function] (Name, FullQualifiedName, Action) values ('Empleados - Editar', 'Bagge.Seti.WebSite.EmployeeEditor', 'U')
insert into [Function] (Name, FullQualifiedName, Action) values ('Empleados - Ver', 'Bagge.Seti.WebSite.EmployeeEditor', 'R')

insert into [Function] (Name, FullQualifiedName, Action) values ('Roles - Listar', 'Bagge.Seti.WebSite.RoleList', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Roles - Borrar', 'Bagge.Seti.WebSite.RoleList', 'D')
insert into [Function] (Name, FullQualifiedName, Action) values ('Roles - Crear', 'Bagge.Seti.WebSite.RoleEditor', 'C')
insert into [Function] (Name, FullQualifiedName, Action) values ('Roles - Editar', 'Bagge.Seti.WebSite.RoleEditor', 'U')
insert into [Function] (Name, FullQualifiedName, Action) values ('Roles - Ver', 'Bagge.Seti.WebSite.RoleEditor', 'R')

insert into [Function] (Name, FullQualifiedName, Action) values ('Tickets - Listar', 'Bagge.Seti.WebSite.TicketList', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Tickets - Borrar', 'Bagge.Seti.WebSite.TicketList', 'D')
insert into [Function] (Name, FullQualifiedName, Action) values ('Tickets - Crear', 'Bagge.Seti.WebSite.TicketEditor', 'C')
insert into [Function] (Name, FullQualifiedName, Action) values ('Tickets - Editar', 'Bagge.Seti.WebSite.TicketEditor', 'U')
insert into [Function] (Name, FullQualifiedName, Action) values ('Tickets - Ver', 'Bagge.Seti.WebSite.TicketEditor', 'R')


insert into [Function] (Name, FullQualifiedName, Action) values ('Clientes - Listar', 'Bagge.Seti.WebSite.CustomerList', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Clientes - Borrar', 'Bagge.Seti.WebSite.CustomerList', 'D')
insert into [Function] (Name, FullQualifiedName, Action) values ('Clientes - Crear', 'Bagge.Seti.WebSite.CustomerEditor', 'C')
insert into [Function] (Name, FullQualifiedName, Action) values ('Clientes - Editar', 'Bagge.Seti.WebSite.CustomerEditor', 'U')
insert into [Function] (Name, FullQualifiedName, Action) values ('Clientes - Ver', 'Bagge.Seti.WebSite.CustomerEditor', 'R')


/*
insert into [Function] (Name, FullQualifiedName, Action) values ('Funciones - Listar', 'Bagge.Seti.WebSite.FunctionList', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Funciones - Borrar', 'Bagge.Seti.WebSite.FunctionList', 'D')
insert into [Function] (Name, FullQualifiedName, Action) values ('Funciones - Crear', 'Bagge.Seti.WebSite.FunctionEditor', 'C')
insert into [Function] (Name, FullQualifiedName, Action) values ('Funciones - Editar', 'Bagge.Seti.WebSite.FunctionEditor', 'U')
insert into [Function] (Name, FullQualifiedName, Action) values ('Funciones - Ver', 'Bagge.Seti.WebSite.FunctionEditor', 'R')
*/

insert into [Function] (Name, FullQualifiedName, Action) values ('Reporte - Clientes por Abono','Bagge.Seti.WebSite.Reports.CustomersBySubscription', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Reporte - Clientes por Ticket','Bagge.Seti.WebSite.Reports.CustomersByTicket', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Reporte - Clientes Pendientes de Pago','Bagge.Seti.WebSite.Reports.CustomersWithPendingPayment', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Reporte - Productos por Proveedor','Bagge.Seti.WebSite.Reports.ProductsByProvider', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Reporte - Productos Consumidos','Bagge.Seti.WebSite.Reports.ProductsConsumed', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Reporte - Roles por Usuario','Bagge.Seti.WebSite.Reports.RolesByUser', 'R')
insert into [Function] (Name, FullQualifiedName, Action) values ('Reporte - Técnicos por Ticket','Bagge.Seti.WebSite.Reports.TechniciansByTicket', 'R')

