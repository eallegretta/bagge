insert into SecureEntity (FunctionId, AssemblyName, ClassFullQualifiedName)
select Id, 'Bagge.Seti.BusinessEntities','Bagge.Seti.BusinessEntities.Provider' from [Function] where FullQualifiedName like '%WebSite.Provider%'

insert into SecureEntity (FunctionId, AssemblyName, ClassFullQualifiedName)
select Id, 'Bagge.Seti.BusinessEntities','Bagge.Seti.BusinessEntities.Product' from [Function] where FullQualifiedName like '%WebSite.Product%'

insert into SecureEntity (FunctionId, AssemblyName, ClassFullQualifiedName)
select Id, 'Bagge.Seti.BusinessEntities','Bagge.Seti.BusinessEntities.Employee' from [Function] where FullQualifiedName like '%WebSite.Employee%'

insert into SecureEntity (FunctionId, AssemblyName, ClassFullQualifiedName)
select Id, 'Bagge.Seti.BusinessEntities','Bagge.Seti.Security.BusinessEntities.Role' from [Function] where FullQualifiedName like '%WebSite.Role%'

insert into SecureEntity (FunctionId, AssemblyName, ClassFullQualifiedName)
select Id, 'Bagge.Seti.BusinessEntities','Bagge.Seti.BusinessEntities.Ticket' from [Function] where FullQualifiedName like '%WebSite.Ticket%'

insert into SecureEntity (FunctionId, AssemblyName, ClassFullQualifiedName)
select Id, 'Bagge.Seti.BusinessEntities','Bagge.Seti.BusinessEntities.Customer' from [Function] where FullQualifiedName like '%WebSite.Customer%'