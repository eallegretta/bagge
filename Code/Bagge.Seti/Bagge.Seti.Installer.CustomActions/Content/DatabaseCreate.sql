/****** Object:  Table [dbo].[AccessibilityType]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccessibilityType](
	[Id] [tinyint] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AccessibilityType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlertConfiguration]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlertConfiguration](
	[Days] [int] NOT NULL,
	[SendEmailToEmployees] [bit] NOT NULL,
	[SendEmailToCreator] [bit] NOT NULL,
	[LastSentDate] [datetime] NULL,
	[MaxDaysPendingAproval] [int] NOT NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_AlertConfiguration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CountryState]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CountryState](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ShortName] [char](2) NOT NULL,
 CONSTRAINT [PK_CountryState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmployeeCategory]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmployeeCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](max) NOT NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[AuditUserName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL CONSTRAINT [DF_EmployeeCategory_Deleted]  DEFAULT ((0)),
 CONSTRAINT [PK_EmployeeCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Function]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Function](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[FullQualifiedName] [varchar](255) NOT NULL,
	[Action] [char](1) NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[AuditUserName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL CONSTRAINT [DF_Product_Deleted]  DEFAULT ((0)),
 CONSTRAINT [PK_tbl_Insumos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProviderCalification]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProviderCalification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_ProviderCalification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[AuditUserName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL CONSTRAINT [DF_Role_Deleted]  DEFAULT ((0)),
	[Description] [varchar](255) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TicketStatus]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TicketStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_tbl_Estado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[TicketsClosedReport]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*SELECCIONAR LOS TICKETS CERRADOS EN UN RANGO DE FECHAS Y MOSTRAR ADMINISTRATIVO (CREADOR DEL TICKET), 
TÉCNICO, CANTIDAD DE TICKETS, CANTIDAD DE HORAS PRESUPUESTADAS, CANTIDAD DE HORAS REALES, PORCENTAJE DE DESVÍO.
PERMITIR AGRUPAR POR TECNICO O POR ADMINISTRATIVO 

EL DESVÍO SE CALCULA DE LA SIGUIENTE MANERA EN TODAS LAS LINEAS, INCLUSO TOTALES:
	SI HORAS PRESUPUESTADAS >< 0:
	DESVÍO = (HORAS REALES – HORAS PRESUPUESTADAS) * 100  / HORAS PRESUPUESTADAS
	SI HORAS PRESUPUESTADAS = 0:
	DESVÍO = 0%
*/

CREATE procedure [dbo].[TicketsClosedReport]
@dateFrom datetime,
@dateTo datetime,
@groupByCreator bit
as

create table #tickets (
	Admin varchar(100),
	Technician varchar(100),
	Tickets int,
	EstimatedDuration decimal(8,2),
	RealDuration decimal(8,2),
	Detour decimal(8,2) )


if @groupByCreator = 1
begin
	insert into #tickets
	select	C.Lastname + ', ' + C.Firstname,
			E.Lastname + ', ' + E.Firstname,
			count(T.Id),
			sum(EstimatedDuration),
			sum(RealDuration),
			case 
				when sum(EstimatedDuration) > 0 then (((sum(RealDuration) - sum(EstimatedDuration)) *  100) / sum(EstimatedDuration))
				when sum(EstimatedDuration) = 0 then 0
			end
	from	Ticket T 
				inner join Employee C on T.EmployeeCreatorId = C.Id
				inner join TicketEmployee TE on T.Id = TE.TicketId
				inner join Employee E on TE.EmployeeId = E.Id
	where	
			T.Deleted = 0 and
			C.Deleted = 0 and
			E.Deleted = 0 and
			T.TicketStatusId = 6 and
			T.ExecutionDateTime between @dateFrom and @dateTo
	group by	C.Lastname + ', ' + C.Firstname, E.Lastname + ', ' + E.Firstname
	order by 	C.Lastname + ', ' + C.Firstname, E.Lastname + ', ' + E.Firstname
	
	select 
		Admin 'Administrativo',
		Technician 'Técnico',
		Tickets,
		EstimatedDuration 'Duración Estimada',
		RealDuration 'Duración Real',
		Detour 'Desvio'	
	from #tickets
	union
	select	Admin + ' - Total',
			'',
			sum(Tickets),
			sum(EstimatedDuration),
			sum(RealDuration),
			case 
				when sum(EstimatedDuration) > 0 then (((sum(RealDuration) - sum(EstimatedDuration)) *  100) / sum(EstimatedDuration))
				when sum(EstimatedDuration) = 0 then 0
			end
	from #tickets
	group by	Admin
	order by	Admin
end
else
begin
insert into #tickets
	select	C.Lastname + ', ' + C.Firstname,
			E.Lastname + ', ' + E.Firstname,
			count(T.Id),
			sum(EstimatedDuration),
			sum(RealDuration),
			case 
				when sum(EstimatedDuration) > 0 then (((sum(RealDuration) - sum(EstimatedDuration)) *  100) / sum(EstimatedDuration))
				when sum(EstimatedDuration) = 0 then 0
			end
	from	Ticket T 
				inner join Employee C on T.EmployeeCreatorId = C.Id
				inner join TicketEmployee TE on T.Id = TE.TicketId
				inner join Employee E on TE.EmployeeId = E.Id
	where	
			T.Deleted = 0 and
			C.Deleted = 0 and
			E.Deleted = 0 and
			T.TicketStatusId = 6 and
			T.ExecutionDateTime between @dateFrom and @dateTo
	group by	E.Lastname + ', ' + E.Firstname, C.Lastname + ', ' + C.Firstname
	order by 	E.Lastname + ', ' + E.Firstname, C.Lastname + ', ' + C.Firstname
	
	select 
		Technician 'Técnico',
		Admin 'Administrativo',
		Tickets,
		EstimatedDuration 'Duración Estimada',
		RealDuration 'Duración Real',
		Detour 'Desvio'	
	from #tickets
	union
	select	Technician + ' - Total',
			'',
			sum(Tickets),
			sum(EstimatedDuration),
			sum(RealDuration),
			case 
				when sum(EstimatedDuration) > 0 then (((sum(RealDuration) - sum(EstimatedDuration)) *  100) / sum(EstimatedDuration))
				when sum(EstimatedDuration) = 0 then 0
			end
	from #tickets
	group by	Technician
	order by	Technician
end
drop table #tickets
GO
/****** Object:  Table [dbo].[District]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ticket](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[TicketStatusId] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ExecutionDateTime] [datetime] NULL,
	[EstimatedDuration] [decimal](18, 2) NOT NULL,
	[RealDuration] [decimal](18, 2) NULL,
	[Description] [varchar](max) NOT NULL,
	[Budget] [decimal](18, 2) NULL,
	[EmployeeCreatorId] [int] NOT NULL,
	[AuditTimeStamp] [timestamp] NULL,
	[AuditUserName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL CONSTRAINT [DF_Ticket_Deleted]  DEFAULT ((0)),
	[Notes] [varchar](max) NULL,
 CONSTRAINT [PK_tbl_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Provider]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Provider](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CUIT] [varchar](50) NULL,
	[Company] [varchar](50) NULL,
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
	[Deleted] [bit] NOT NULL CONSTRAINT [DF_Provider_Deleted]  DEFAULT ((0)),
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_Proveedores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[Deleted] [bit] NOT NULL CONSTRAINT [DF_Customer_Deleted]  DEFAULT ((0)),
 CONSTRAINT [PK_tbl_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleEmployee]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleEmployee](
	[RoleId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_RoleEmployee] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketEmployee]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketEmployee](
	[TicketId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_TicketEmployee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[Deleted] [bit] NOT NULL CONSTRAINT [DF_Employee_Deleted]  DEFAULT ((0)),
	[RecoverPasswordKey] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_Empleados] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleFunction]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleFunction](
	[RoleId] [int] NOT NULL,
	[FunctionId] [int] NOT NULL,
 CONSTRAINT [PK_RoleFunction] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[FunctionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecureEntity]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SecureEntity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FunctionId] [int] NOT NULL,
	[ClassFullQualifiedName] [varchar](255) NOT NULL,
 CONSTRAINT [PK_SecureEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductProvider]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[ProductTicket]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTicket](
	[TicketId] [int] NULL,
	[EstimatedQuantity] [decimal](18, 2) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductProviderId] [int] NOT NULL,
 CONSTRAINT [PK_ProductTicket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecurityException]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SecurityException](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConstraintType] [varchar](4) NOT NULL,
	[Value] [varchar](255) NULL,
	[RoleId] [int] NOT NULL,
	[SecureEntityId] [int] NOT NULL,
	[PropertyName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SecurityException] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[CustomersBySubscriptionReport]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* SELECCIONAR TODOS LOS CLIENTES C/ABONO Y  S/ABONO. ORDEN POR ABONO DESC. */
CREATE procedure [dbo].[CustomersBySubscriptionReport]

as

	
select  C.Name 'Nombre',
		C.CUIT 'CUIT',
		C.Address 'Dirección',
		C.Floor 'Piso',
		C.Departament 'Deptartamento',
		C.ZipCode 'Código Postal',
		C.City 'Ciudad',
		C.Phone 'Teléfono',
		C.MobilePhone 'Celular',
		C.Email 'E-Mail',
		C.Subscription 'Abonado'

from Customer C    

where	C.Deleted = 0

order by C.Subscription,C.Name desc
GO
/****** Object:  StoredProcedure [dbo].[CustomersWithPendingPaymentReport]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* SELECCIONAR TODOS LOS CLIENTES QUE ESTAN PENDIENTE DE PAGO 
ORDENAR POR LO QUE DEBE Y DESCENDENTE, ESPECIFICANDO SI ES C/ABONO O  S/ABONO */

CREATE procedure [dbo].[CustomersWithPendingPaymentReport]
as

select  C.Name 'Nombre',
		C.CUIT 'CUIT',
		C.Address 'Dirección',
		C.Floor 'Piso',
		C.Departament 'Deptartamento',
		C.ZipCode 'Código Postal',
		C.City 'Ciudad',
		C.Phone 'Teléfono',
		C.MobilePhone 'Celular',
		T.Budget 'Presupuesto',
		C.Email 'E-Mail',
		C.Subscription 'Abonado'




from Customer C inner join Ticket T on T.CustomerId = C.Id     

where	C.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId = 4

order by T.Budget desc
GO
/****** Object:  StoredProcedure [dbo].[CustomersByTicketReport]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* SELECCIONAR TODOS LOS CLIENTES POR CANTIDAD DE TICKETS EJECUTADOS, 
HORAS CONSUMIDAS Y “BUDGET” C/ABONO Y  S/ABONO, QUE SE EJECUTARON DURANTE 
TODO EL MES (O DESDE HASTA). ORDEN ASCENDENTE POR CLIENTE */
 
CREATE procedure [dbo].[CustomersByTicketReport]

@dateFrom datetime=null,
@dateTo datetime=null

as

if ((@dateFrom is null) and (@dateTo is null))
begin
	select 		C.Name	'Nombre',
				C.CUIT	'CUIT',
				count(T.Id) 'Cantidad de Tickets',
				sum(T.RealDuration)	'Total Duración Real',
				sum(T.Budget) 'Total Presupuestos',
				C.Subscription 'Abonado'

	from  Customer C inner join Ticket T on  C.Id = T.CustomerId
								
	where	C.Deleted = 0 and
			T.Deleted = 0 and
			T.TicketStatusId= 6

	group by    C.Name,
				C.CUIT,
				C.Subscription

	order by	C.Name,
				C.CUIT
end

if ((@dateFrom is not null) and (@dateTo is not null))
begin
select 		C.Name	'Nombre',
				C.CUIT	'CUIT',
				count(T.Id) 'Cantidad de Tickets',
				sum(T.RealDuration)	'Total Duración Real',
				sum(T.Budget) 'Total Presupuestos',
				C.Subscription 'Abonado'

	from  Customer C inner join Ticket T on  C.Id = T.CustomerId
								
	where	C.Deleted = 0 and
			T.Deleted = 0 and
			T.TicketStatusId= 6
			and T.ExecutionDateTime between @dateFrom and @dateTo

	group by    C.Name,
				C.CUIT,
				C.Subscription

	order by	C.Name,
				C.CUIT
end
GO
/****** Object:  StoredProcedure [dbo].[TechniciansByTicketReport]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* SELECCIONAR TODOS LOS TECNICOS DE ACUERDO A LA CANTIDAD DE TICKETS QUE EJECUTARON 
Y HORAS CONSUMIDAS DURANTE TODO EL MES (O DESDE HASTA) */

CREATE procedure [dbo].[TechniciansByTicketReport]
@dateFrom datetime,
@dateTo datetime
as

select	E.Firstname 'Nombre',
		E.Lastname 'Apellido',
		count(TE.Id) 'Cantidad de Tickets',
		sum(T.RealDuration) 'Total Duracion Real'

from  dbo.TicketEmployee TE inner join Employee E on TE.EmployeeId = E.Id
							inner join Ticket T on TE.TicketId = T.Id
						
where	E.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId= 6 and 
		T.ExecutionDateTime between @dateFrom and @dateTo

GROUP BY	E.Id,
			E.Firstname,
			E.Lastname
GO
/****** Object:  StoredProcedure [dbo].[RolesByUserReport]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Crear un reporte de roles asignados a usuarios, permitiendo filtrar por rol.*/
CREATE procedure [dbo].[RolesByUserReport]

@name varchar(255)=null,
@description varchar(255)=null

as

select	R.[Name]			'Nombre Rol',
		R.Description		'Descripción Rol',

		E.Username			'Nombre de Usuario',
		E.FileNumber		'Legajo',
		E.Firstname			'Nombre',
		E.Lastname			'Apellido',
		E.Phone				'Teléfono',
		E.EmergencyPhone	'Teléfono de Emergencia',
		E.Email				'E-mail'

from	RoleEmployee RE	
		
		inner join	[Role] R	on RE.RoleId = R.Id
		inner join	Employee E	on RE.EmployeeId = E.Id

where	

E.Deleted = 0 and
R.Deleted = 0 and
		
(
	(
		(R.[Name] like '%' + @name + '%') or (@name is null)
	)			and 
	(
		(R.Description like '%' + @description + '%') or (@description is 
null)
	)
)
				
order by R.[Name],E.FileNumber
GO
/****** Object:  StoredProcedure [dbo].[ProductsByProviderReport]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* SELECCIONAR TODOS LOS INSUMOS DE MENOR PRECIO POR PROVEEDOR, ORDEN ASCENDENTE POR INSUMO. */
CREATE procedure [dbo].[ProductsByProviderReport]
as
select	PROD.Name 'Nombre',
		PROD.Description 'Descripción',
		min(PP.UnitaryPrice) 'Menor Precio',
		PROV.Company 'Compañía'

from  ProductProvider PP
		inner join Product PROD on  PP.ProductId = PROD.Id
		inner join Provider PROV on PP.ProviderId = PROV.Id
where	PROD.Deleted = 0 and
		PROV.Deleted = 0 

GROUP BY	PROD.Name,
			PROD.Description,
			PROV.Company

order by	PROD.Name,
			PROD.Description
GO
/****** Object:  StoredProcedure [dbo].[ProductsConsumedReport]    Script Date: 10/12/2009 03:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* SELECCIONAR TODOS LOS INSUMOS Y CANTIDAD QUE SE CONSUMIERON 
DURANTE TODO EL MES (O DESDE HASTA) ORDEN DESCENDENTE */

CREATE procedure [dbo].[ProductsConsumedReport]

@dateFrom datetime,
@dateTo datetime

as

select	P.Name 'Nombre',
		P.Description 'Descripción',
		sum(PT.EstimatedQuantity) 'Total Cantidad Estimada'		

from  ProductTicket PT 
			inner join ProductProvider PP on PT.ProductProviderId = PP.Id
			inner join Product P on PP.ProductId = P.Id
			inner join Ticket T on PT.TicketId = T.Id 
where	P.Deleted = 0 and
		T.Deleted = 0 and
		T.TicketStatusId= 6 and 
		T.ExecutionDateTime between @dateFrom and @dateTo
		
GROUP BY	P.Name,
			P.Description

order by sum(PT.EstimatedQuantity) desc
GO
/****** Object:  ForeignKey [FK_Customer_District]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_District] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_District]
GO
/****** Object:  ForeignKey [FK_City_CountryState]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_City_CountryState] FOREIGN KEY([CountryStateId])
REFERENCES [dbo].[CountryState] ([Id])
GO
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_City_CountryState]
GO
/****** Object:  ForeignKey [FK_Employee_EmployeeCategory]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_EmployeeCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[EmployeeCategory] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_EmployeeCategory]
GO
/****** Object:  ForeignKey [FK_ProductProvider_Product]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[ProductProvider]  WITH CHECK ADD  CONSTRAINT [FK_ProductProvider_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ProductProvider] CHECK CONSTRAINT [FK_ProductProvider_Product]
GO
/****** Object:  ForeignKey [FK_ProductProvider_Provider]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[ProductProvider]  WITH CHECK ADD  CONSTRAINT [FK_ProductProvider_Provider] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Provider] ([Id])
GO
ALTER TABLE [dbo].[ProductProvider] CHECK CONSTRAINT [FK_ProductProvider_Provider]
GO
/****** Object:  ForeignKey [FK_ProductTicket_ProductProvider]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[ProductTicket]  WITH CHECK ADD  CONSTRAINT [FK_ProductTicket_ProductProvider] FOREIGN KEY([ProductProviderId])
REFERENCES [dbo].[ProductProvider] ([Id])
GO
ALTER TABLE [dbo].[ProductTicket] CHECK CONSTRAINT [FK_ProductTicket_ProductProvider]
GO
/****** Object:  ForeignKey [FK_ProductTicket_Ticket]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[ProductTicket]  WITH CHECK ADD  CONSTRAINT [FK_ProductTicket_Ticket] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Ticket] ([Id])
GO
ALTER TABLE [dbo].[ProductTicket] CHECK CONSTRAINT [FK_ProductTicket_Ticket]
GO
/****** Object:  ForeignKey [FK_Provider_District]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[Provider]  WITH CHECK ADD  CONSTRAINT [FK_Provider_District] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([Id])
GO
ALTER TABLE [dbo].[Provider] CHECK CONSTRAINT [FK_Provider_District]
GO
/****** Object:  ForeignKey [FK_Provider_ProviderCalification]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[Provider]  WITH CHECK ADD  CONSTRAINT [FK_Provider_ProviderCalification] FOREIGN KEY([CalificationId])
REFERENCES [dbo].[ProviderCalification] ([Id])
GO
ALTER TABLE [dbo].[Provider] CHECK CONSTRAINT [FK_Provider_ProviderCalification]
GO
/****** Object:  ForeignKey [FK_RoleEmployee_Employee]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[RoleEmployee]  WITH CHECK ADD  CONSTRAINT [FK_RoleEmployee_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[RoleEmployee] CHECK CONSTRAINT [FK_RoleEmployee_Employee]
GO
/****** Object:  ForeignKey [FK_RoleEmployee_Role]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[RoleEmployee]  WITH CHECK ADD  CONSTRAINT [FK_RoleEmployee_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[RoleEmployee] CHECK CONSTRAINT [FK_RoleEmployee_Role]
GO
/****** Object:  ForeignKey [FK_RoleFunction_Function]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[RoleFunction]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunction_Function] FOREIGN KEY([FunctionId])
REFERENCES [dbo].[Function] ([Id])
GO
ALTER TABLE [dbo].[RoleFunction] CHECK CONSTRAINT [FK_RoleFunction_Function]
GO
/****** Object:  ForeignKey [FK_RoleFunction_Role]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[RoleFunction]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunction_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[RoleFunction] CHECK CONSTRAINT [FK_RoleFunction_Role]
GO
/****** Object:  ForeignKey [FK_SecureEntity_Function]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[SecureEntity]  WITH CHECK ADD  CONSTRAINT [FK_SecureEntity_Function] FOREIGN KEY([FunctionId])
REFERENCES [dbo].[Function] ([Id])
GO
ALTER TABLE [dbo].[SecureEntity] CHECK CONSTRAINT [FK_SecureEntity_Function]
GO
/****** Object:  ForeignKey [FK_SecuriyException_Role]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[SecurityException]  WITH CHECK ADD  CONSTRAINT [FK_SecuriyException_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[SecurityException] CHECK CONSTRAINT [FK_SecuriyException_Role]
GO
/****** Object:  ForeignKey [FK_SecuriyException_SecureEntity]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[SecurityException]  WITH CHECK ADD  CONSTRAINT [FK_SecuriyException_SecureEntity] FOREIGN KEY([SecureEntityId])
REFERENCES [dbo].[SecureEntity] ([Id])
GO
ALTER TABLE [dbo].[SecurityException] CHECK CONSTRAINT [FK_SecuriyException_SecureEntity]
GO
/****** Object:  ForeignKey [FK_Ticket_Customer]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Customer]
GO
/****** Object:  ForeignKey [FK_Ticket_Employee]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Employee] FOREIGN KEY([EmployeeCreatorId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Employee]
GO
/****** Object:  ForeignKey [FK_Ticket_TicketStatus]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_TicketStatus] FOREIGN KEY([TicketStatusId])
REFERENCES [dbo].[TicketStatus] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_TicketStatus]
GO
/****** Object:  ForeignKey [FK_TicketEmployee_Employee1]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[TicketEmployee]  WITH CHECK ADD  CONSTRAINT [FK_TicketEmployee_Employee1] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[TicketEmployee] CHECK CONSTRAINT [FK_TicketEmployee_Employee1]
GO
/****** Object:  ForeignKey [FK_TicketEmployee_Ticket]    Script Date: 10/12/2009 03:14:40 ******/
ALTER TABLE [dbo].[TicketEmployee]  WITH CHECK ADD  CONSTRAINT [FK_TicketEmployee_Ticket] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Ticket] ([Id])
GO
ALTER TABLE [dbo].[TicketEmployee] CHECK CONSTRAINT [FK_TicketEmployee_Ticket]
GO
