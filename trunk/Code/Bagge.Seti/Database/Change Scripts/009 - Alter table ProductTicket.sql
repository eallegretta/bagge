alter table ProductTicket drop FK_ProductTicket_Product1
go
alter table ProductTicket drop column ProductId
go
alter table ProductTicket add ProductProviderId int not null
go
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductTicket_ProductProvider]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductTicket]'))
ALTER TABLE [dbo].[ProductTicket]  WITH CHECK ADD  CONSTRAINT [FK_ProductTicket_ProductProvider] FOREIGN KEY([ProductProviderId])
REFERENCES [dbo].[ProductProvider] ([Id])
GO
