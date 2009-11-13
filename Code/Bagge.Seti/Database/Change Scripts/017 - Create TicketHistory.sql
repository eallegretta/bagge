/****** Object:  Table [dbo].[TicketHistory]    Script Date: 11/13/2009 00:37:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketHistory]') AND type in (N'U'))
DROP TABLE [dbo].[TicketHistory]
GO
/****** Object:  Table [dbo].[TicketHistory]    Script Date: 11/13/2009 00:37:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TicketHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TicketId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Notes] [varchar](max) NULL,
	[TicketStatusId] [int] NOT NULL,
 CONSTRAINT [PK_TicketHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketHistory_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketHistory]'))
ALTER TABLE [dbo].[TicketHistory]  WITH CHECK ADD  CONSTRAINT [FK_TicketHistory_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketHistory_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketHistory]'))
ALTER TABLE [dbo].[TicketHistory] CHECK CONSTRAINT [FK_TicketHistory_Employee]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketHistory_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketHistory]'))
ALTER TABLE [dbo].[TicketHistory]  WITH CHECK ADD  CONSTRAINT [FK_TicketHistory_Ticket] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Ticket] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketHistory_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketHistory]'))
ALTER TABLE [dbo].[TicketHistory] CHECK CONSTRAINT [FK_TicketHistory_Ticket]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketHistory_TicketStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketHistory]'))
ALTER TABLE [dbo].[TicketHistory]  WITH CHECK ADD  CONSTRAINT [FK_TicketHistory_TicketStatus] FOREIGN KEY([TicketStatusId])
REFERENCES [dbo].[TicketStatus] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketHistory_TicketStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketHistory]'))
ALTER TABLE [dbo].[TicketHistory] CHECK CONSTRAINT [FK_TicketHistory_TicketStatus]
GO
