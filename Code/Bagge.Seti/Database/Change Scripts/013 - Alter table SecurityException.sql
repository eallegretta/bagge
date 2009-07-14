alter table SecurityException drop FK_SecuriyException_Function
go
alter table SecurityException drop column FunctionId
go
alter table SecurityException add RoleId int not null
go
ALTER TABLE [dbo].[SecurityException]  WITH CHECK ADD  CONSTRAINT [FK_SecuriyException_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])

