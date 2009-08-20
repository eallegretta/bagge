IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Customer_District]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_District]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF_Customer_Deleted]
END

GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_City_CountryState]') AND parent_object_id = OBJECT_ID(N'[dbo].[District]'))
ALTER TABLE [dbo].[District] DROP CONSTRAINT [FK_City_CountryState]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_EmployeeCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK_Employee_EmployeeCategory]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Employee_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [DF_Employee_Deleted]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EmployeeCategory_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EmployeeCategory] DROP CONSTRAINT [DF_EmployeeCategory_Deleted]
END

GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Function_Function]') AND parent_object_id = OBJECT_ID(N'[dbo].[Function]'))
ALTER TABLE [dbo].[Function] DROP CONSTRAINT [FK_Function_Function]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Function_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Function] DROP CONSTRAINT [DF_Function_Deleted]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Product_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [DF_Product_Deleted]
END

GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductProvider_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductProvider]'))
ALTER TABLE [dbo].[ProductProvider] DROP CONSTRAINT [FK_ProductProvider_Product]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductProvider_Provider]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductProvider]'))
ALTER TABLE [dbo].[ProductProvider] DROP CONSTRAINT [FK_ProductProvider_Provider]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductTicket_Product1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductTicket]'))
ALTER TABLE [dbo].[ProductTicket] DROP CONSTRAINT [FK_ProductTicket_Product1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductTicket_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductTicket]'))
ALTER TABLE [dbo].[ProductTicket] DROP CONSTRAINT [FK_ProductTicket_Ticket]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Provider_District]') AND parent_object_id = OBJECT_ID(N'[dbo].[Provider]'))
ALTER TABLE [dbo].[Provider] DROP CONSTRAINT [FK_Provider_District]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Provider_ProviderCalification]') AND parent_object_id = OBJECT_ID(N'[dbo].[Provider]'))
ALTER TABLE [dbo].[Provider] DROP CONSTRAINT [FK_Provider_ProviderCalification]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Provider_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Provider] DROP CONSTRAINT [DF_Provider_Deleted]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Role_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Role_Deleted]
END

GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleEmployee_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleEmployee]'))
ALTER TABLE [dbo].[RoleEmployee] DROP CONSTRAINT [FK_RoleEmployee_Employee]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleEmployee_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleEmployee]'))
ALTER TABLE [dbo].[RoleEmployee] DROP CONSTRAINT [FK_RoleEmployee_Role]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunction_Function]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunction]'))
ALTER TABLE [dbo].[RoleFunction] DROP CONSTRAINT [FK_RoleFunction_Function]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunction_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunction]'))
ALTER TABLE [dbo].[RoleFunction] DROP CONSTRAINT [FK_RoleFunction_Role]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK_Ticket_Customer]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK_Ticket_Employee]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_TicketStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK_Ticket_TicketStatus]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Ticket_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [DF_Ticket_Deleted]
END

GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketEmployee_Employee1]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketEmployee]'))
ALTER TABLE [dbo].[TicketEmployee] DROP CONSTRAINT [FK_TicketEmployee_Employee1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketEmployee_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketEmployee]'))
ALTER TABLE [dbo].[TicketEmployee] DROP CONSTRAINT [FK_TicketEmployee_Ticket]
GO
/****** Object:  Table [dbo].[AccessibilityType]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccessibilityType]') AND type in (N'U'))
DROP TABLE [dbo].[AccessibilityType]
GO
/****** Object:  Table [dbo].[AlertConfiguration]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlertConfiguration]') AND type in (N'U'))
DROP TABLE [dbo].[AlertConfiguration]
GO
/****** Object:  Table [dbo].[CountryState]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountryState]') AND type in (N'U'))
DROP TABLE [dbo].[CountryState]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO
/****** Object:  Table [dbo].[District]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[District]') AND type in (N'U'))
DROP TABLE [dbo].[District]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
DROP TABLE [dbo].[Employee]
GO
/****** Object:  Table [dbo].[EmployeeCategory]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeCategory]') AND type in (N'U'))
DROP TABLE [dbo].[EmployeeCategory]
GO
/****** Object:  Table [dbo].[Function]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Function]') AND type in (N'U'))
DROP TABLE [dbo].[Function]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
DROP TABLE [dbo].[Product]
GO
/****** Object:  Table [dbo].[ProductProvider]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductProvider]') AND type in (N'U'))
DROP TABLE [dbo].[ProductProvider]
GO
/****** Object:  Table [dbo].[ProductTicket]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductTicket]') AND type in (N'U'))
DROP TABLE [dbo].[ProductTicket]
GO
/****** Object:  Table [dbo].[Provider]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Provider]') AND type in (N'U'))
DROP TABLE [dbo].[Provider]
GO
/****** Object:  Table [dbo].[ProviderCalification]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderCalification]') AND type in (N'U'))
DROP TABLE [dbo].[ProviderCalification]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[RoleEmployee]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleEmployee]') AND type in (N'U'))
DROP TABLE [dbo].[RoleEmployee]
GO
/****** Object:  Table [dbo].[RoleFunction]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleFunction]') AND type in (N'U'))
DROP TABLE [dbo].[RoleFunction]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket]') AND type in (N'U'))
DROP TABLE [dbo].[Ticket]
GO
/****** Object:  Table [dbo].[TicketEmployee]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketEmployee]') AND type in (N'U'))
DROP TABLE [dbo].[TicketEmployee]
GO
/****** Object:  Table [dbo].[TicketStatus]    Script Date: 04/16/2009 02:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketStatus]') AND type in (N'U'))
DROP TABLE [dbo].[TicketStatus]
GO
/****** Object:  Table [dbo].[AccessibilityType]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccessibilityType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccessibilityType](
	[Id] [tinyint] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AccessibilityType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlertConfiguration]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlertConfiguration]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AlertConfiguration](
	[Days] [int] NOT NULL,
	[SendEmailToEmployees] [bit] NOT NULL,
	[SendEmailToCreator] [bit] NOT NULL,
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CountryState]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountryState]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CountryState](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ShortName] [char](2) NOT NULL,
 CONSTRAINT [PK_CountryState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CUIT] [varchar](50) NULL,
	[DistrictId] [int] NOT NULL,
	[Address] [varchar](50) NULL,
	[Floor] [char](1) NULL,
	[Departament] [char](1) NULL,
	[ZipCode] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[MobilePhone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Subscription] [bit] NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[AuditUserName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_tbl_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[District]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[District]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[District](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryStateId] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[ZipCode] [varchar](10) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[FileNumber] [varchar](50) NULL,
	[Firstname] [varchar](50) NULL,
	[Lastname] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[EmergencyPhone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[AuditUserName] [varchar](50) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_tbl_Empleados] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmployeeCategory]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmployeeCategory](
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](max) NOT NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[AuditUserName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_EmployeeCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Function]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Function]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Function](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Assembly] [varchar](255) NOT NULL,
	[ClassFullQualifiedName] [varchar](255) NOT NULL,
	[MemberName] [varchar](255) NOT NULL,
	[MemberType] [char](1) NOT NULL,
	[AccessibilityTypeId] [tinyint] NOT NULL,
	[ConstraintType] [char](1) NOT NULL,
	[Value] [varbinary](50) NOT NULL,
	[AuditUserName] [varchar](50) NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[AuditUserName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_tbl_Insumos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductProvider]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductProvider]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductProvider](
	[ProductId] [int] NOT NULL,
	[ProviderId] [int] NOT NULL,
	[UnitaryPrice] [decimal](18, 2) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ProductProvider] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductTicket]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductTicket]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductTicket](
	[ProductId] [int] NOT NULL,
	[TicketId] [int] NOT NULL,
	[EstimatedQuantity] [decimal](18, 2) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ProductTicket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Provider]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Provider]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Provider](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CUIT] [varchar](50) NULL,
	[Company] [varchar](50) NOT NULL,
	[Address] [varchar](50) NULL,
	[DistrictId] [int] NOT NULL,
	[Floor] [char](1) NULL,
	[Departament] [char](1) NULL,
	[ZipCode] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Phone] [varchar](50) NOT NULL,
	[SecondaryPhone] [varchar](50) NULL,
	[FaxPhone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[WebSite] [varchar](50) NULL,
	[ContactName] [varchar](50) NOT NULL,
	[CalificationId] [int] NOT NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[AuditUserName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_tbl_Proveedores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProviderCalification]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderCalification]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProviderCalification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_ProviderCalification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[AuditUserName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleEmployee]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleEmployee]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoleEmployee](
	[RoleId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_RoleEmployee] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RoleFunction]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleFunction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoleFunction](
	[RoleId] [int] NOT NULL,
	[FunctionId] [int] NOT NULL,
 CONSTRAINT [PK_RoleFunction] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[FunctionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ticket](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[TicketStatusId] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
	[ExecutionDate] [datetime] NULL,
	[CustomerETA] [datetime] NOT NULL,
	[EstimatedDuration] [decimal](18, 2) NOT NULL,
	[RealDuration] [decimal](18, 2) NULL,
	[Description] [varchar](max) NOT NULL,
	[Budget] [decimal](18, 2) NULL,
	[EmployeeCreatorId] [int] NOT NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[AuditUserName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_tbl_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TicketEmployee]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketEmployee]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TicketEmployee](
	[TicketId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_TicketEmployee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TicketStatus]    Script Date: 04/16/2009 02:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TicketStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_tbl_Estado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Customer_District]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_District] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Customer_District]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_District]
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_Deleted]  DEFAULT ((0)) FOR [Deleted]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_City_CountryState]') AND parent_object_id = OBJECT_ID(N'[dbo].[District]'))
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_City_CountryState] FOREIGN KEY([CountryStateId])
REFERENCES [dbo].[CountryState] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_City_CountryState]') AND parent_object_id = OBJECT_ID(N'[dbo].[District]'))
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_City_CountryState]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_EmployeeCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_EmployeeCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[EmployeeCategory] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_EmployeeCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_EmployeeCategory]
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Employee_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_Deleted]  DEFAULT ((0)) FOR [Deleted]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EmployeeCategory_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EmployeeCategory] ADD  CONSTRAINT [DF_EmployeeCategory_Deleted]  DEFAULT ((0)) FOR [Deleted]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Function_Function]') AND parent_object_id = OBJECT_ID(N'[dbo].[Function]'))
ALTER TABLE [dbo].[Function]  WITH CHECK ADD  CONSTRAINT [FK_Function_Function] FOREIGN KEY([AccessibilityTypeId])
REFERENCES [dbo].[AccessibilityType] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Function_Function]') AND parent_object_id = OBJECT_ID(N'[dbo].[Function]'))
ALTER TABLE [dbo].[Function] CHECK CONSTRAINT [FK_Function_Function]
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Function_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Function] ADD  CONSTRAINT [DF_Function_Deleted]  DEFAULT ((0)) FOR [Deleted]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Product_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Deleted]  DEFAULT ((0)) FOR [Deleted]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductProvider_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductProvider]'))
ALTER TABLE [dbo].[ProductProvider]  WITH CHECK ADD  CONSTRAINT [FK_ProductProvider_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductProvider_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductProvider]'))
ALTER TABLE [dbo].[ProductProvider] CHECK CONSTRAINT [FK_ProductProvider_Product]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductProvider_Provider]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductProvider]'))
ALTER TABLE [dbo].[ProductProvider]  WITH CHECK ADD  CONSTRAINT [FK_ProductProvider_Provider] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Provider] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductProvider_Provider]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductProvider]'))
ALTER TABLE [dbo].[ProductProvider] CHECK CONSTRAINT [FK_ProductProvider_Provider]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductTicket_Product1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductTicket]'))
ALTER TABLE [dbo].[ProductTicket]  WITH CHECK ADD  CONSTRAINT [FK_ProductTicket_Product1] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductTicket_Product1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductTicket]'))
ALTER TABLE [dbo].[ProductTicket] CHECK CONSTRAINT [FK_ProductTicket_Product1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductTicket_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductTicket]'))
ALTER TABLE [dbo].[ProductTicket]  WITH CHECK ADD  CONSTRAINT [FK_ProductTicket_Ticket] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Ticket] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductTicket_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductTicket]'))
ALTER TABLE [dbo].[ProductTicket] CHECK CONSTRAINT [FK_ProductTicket_Ticket]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Provider_District]') AND parent_object_id = OBJECT_ID(N'[dbo].[Provider]'))
ALTER TABLE [dbo].[Provider]  WITH CHECK ADD  CONSTRAINT [FK_Provider_District] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Provider_District]') AND parent_object_id = OBJECT_ID(N'[dbo].[Provider]'))
ALTER TABLE [dbo].[Provider] CHECK CONSTRAINT [FK_Provider_District]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Provider_ProviderCalification]') AND parent_object_id = OBJECT_ID(N'[dbo].[Provider]'))
ALTER TABLE [dbo].[Provider]  WITH CHECK ADD  CONSTRAINT [FK_Provider_ProviderCalification] FOREIGN KEY([CalificationId])
REFERENCES [dbo].[ProviderCalification] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Provider_ProviderCalification]') AND parent_object_id = OBJECT_ID(N'[dbo].[Provider]'))
ALTER TABLE [dbo].[Provider] CHECK CONSTRAINT [FK_Provider_ProviderCalification]
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Provider_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Provider] ADD  CONSTRAINT [DF_Provider_Deleted]  DEFAULT ((0)) FOR [Deleted]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Role_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_Deleted]  DEFAULT ((0)) FOR [Deleted]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleEmployee_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleEmployee]'))
ALTER TABLE [dbo].[RoleEmployee]  WITH CHECK ADD  CONSTRAINT [FK_RoleEmployee_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleEmployee_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleEmployee]'))
ALTER TABLE [dbo].[RoleEmployee] CHECK CONSTRAINT [FK_RoleEmployee_Employee]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleEmployee_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleEmployee]'))
ALTER TABLE [dbo].[RoleEmployee]  WITH CHECK ADD  CONSTRAINT [FK_RoleEmployee_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleEmployee_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleEmployee]'))
ALTER TABLE [dbo].[RoleEmployee] CHECK CONSTRAINT [FK_RoleEmployee_Role]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunction_Function]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunction]'))
ALTER TABLE [dbo].[RoleFunction]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunction_Function] FOREIGN KEY([FunctionId])
REFERENCES [dbo].[Function] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunction_Function]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunction]'))
ALTER TABLE [dbo].[RoleFunction] CHECK CONSTRAINT [FK_RoleFunction_Function]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunction_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunction]'))
ALTER TABLE [dbo].[RoleFunction]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunction_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunction_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunction]'))
ALTER TABLE [dbo].[RoleFunction] CHECK CONSTRAINT [FK_RoleFunction_Role]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Customer]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Employee] FOREIGN KEY([EmployeeCreatorId])
REFERENCES [dbo].[Employee] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Employee]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_TicketStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_TicketStatus] FOREIGN KEY([TicketStatusId])
REFERENCES [dbo].[TicketStatus] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_TicketStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_TicketStatus]
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Ticket_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Ticket] ADD  CONSTRAINT [DF_Ticket_Deleted]  DEFAULT ((0)) FOR [Deleted]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketEmployee_Employee1]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketEmployee]'))
ALTER TABLE [dbo].[TicketEmployee]  WITH CHECK ADD  CONSTRAINT [FK_TicketEmployee_Employee1] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketEmployee_Employee1]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketEmployee]'))
ALTER TABLE [dbo].[TicketEmployee] CHECK CONSTRAINT [FK_TicketEmployee_Employee1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketEmployee_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketEmployee]'))
ALTER TABLE [dbo].[TicketEmployee]  WITH CHECK ADD  CONSTRAINT [FK_TicketEmployee_Ticket] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Ticket] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TicketEmployee_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketEmployee]'))
ALTER TABLE [dbo].[TicketEmployee] CHECK CONSTRAINT [FK_TicketEmployee_Ticket]
GO
