alter table SecurityException drop FK_SecuriyException_Function
go
alter table SecurityException drop column FunctionId
go
alter table SecurityException add RoleId int not null
go
alter table SecurityException alter column [ConstraintType] [varchar](4) NOT NULL
go

ALTER TABLE [dbo].[SecurityException]  WITH CHECK ADD  CONSTRAINT [FK_SecuriyException_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])

