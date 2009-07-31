-- primero pasar todos los empleados desde 
-- la aplicacion que esten como Administrador o Secretaria --> a categoria Tecnico o Administrativo
-- sino va a dar Err el script

delete from dbo.EmployeeCategory
where Name = 'Administrador'

delete from dbo.EmployeeCategory
where Name = 'Secretaria'

