alter table [Function] drop FK_Function_Function
alter table [Function] drop DF_Function_Deleted
alter table [Function] drop column Assembly
alter table [Function] drop column ClassFullQualifiedName
alter table [Function] drop column MemberName
alter table [Function] drop column MemberType
alter table [Function] drop column AccessibilityTypeId
alter table [Function] drop column ConstraintType
alter table [Function] drop column Value
alter table [Function] drop column AuditUserName
alter table [Function] drop column AuditTimeStamp
alter table [Function] drop column Deleted
alter table [Function] add FullQualifiedName varchar(255) not null
alter table [Function] add Action char(1)