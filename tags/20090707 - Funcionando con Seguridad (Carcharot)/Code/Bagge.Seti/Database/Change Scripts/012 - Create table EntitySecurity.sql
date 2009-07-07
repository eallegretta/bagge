CREATE TABLE [dbo].[SecurityException](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Assembly] [varchar](255) NOT NULL,
	[ClassFullQualifiedName] [varchar](255) NOT NULL,
	[MemberName] [varchar](255) NOT NULL,
	[MemberType] [char](1) NOT NULL,
	[AccessibilityTypeId] [tinyint] NOT NULL,
	[ConstraintType] [char](1) NOT NULL,
	[Value] [varbinary](50) NOT NULL,
	[FunctionId] int null
 CONSTRAINT [PK_SecurityException] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
go

ALTER TABLE [dbo].[SecurityException]  WITH CHECK ADD  CONSTRAINT [FK_SecuriyException_Function] FOREIGN KEY([FunctionId])
REFERENCES [dbo].[Function] ([Id])

