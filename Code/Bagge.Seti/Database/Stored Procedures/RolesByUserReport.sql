GO
/****** Object:  StoredProcedure [dbo].[RolesByUserReport]    Script Date: 06/28/2009 01:44:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolesByUserReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RolesByUserReport]
GO
/****** Object:  StoredProcedure [dbo].[RolesByUserReport]    Script Date: 06/28/2009 01:44:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Crear un reporte de roles asignados a usuarios, permitiendo filtrar por rol.*/
CREATE procedure [dbo].[RolesByUserReport]

@name varchar(255)=null,
@description varchar(255)=null,
@userName varchar(50)=null

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
		(R.Description like '%' + @description + '%') or (@description is null)
	)			and	
	(
		(E.UserName like '%' + @userName + '%') or (@userName is null)
	)
)
				
order by R.[Name],E.FileNumber