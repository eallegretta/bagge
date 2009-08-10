begin tran

CREATE TABLE [dbo].[SecureEntity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FunctionId] [int] NOT NULL,
	[AssemblyName] [varchar](255) NOT NULL,
	[ClassFullQualifiedName] [varchar](255) NOT NULL
 CONSTRAINT [PK_SecureEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[SecureEntity]  WITH CHECK ADD  CONSTRAINT [FK_SecureEntity_Function] FOREIGN KEY([FunctionId])
REFERENCES [dbo].[Function] ([Id])

alter table SecurityException drop column Assembly
alter table SecurityException drop column ClassFullQualifiedName
alter table SecurityException drop column MemberType
alter table SecurityException drop column MemberName
alter table SecurityException drop column AccessibilityTypeId

alter table SecurityException add SecureEntityId int NOT NULL
alter table SecurityException add PropertyName varchar(50) NOT NULL

ALTER TABLE [dbo].[SecurityException]  WITH CHECK ADD  CONSTRAINT [FK_SecuriyException_SecureEntity] FOREIGN KEY([SecureEntityId])
REFERENCES [dbo].[SecureEntity] ([Id])





if @@error = 0 
	commit tran
else
	rollback tran