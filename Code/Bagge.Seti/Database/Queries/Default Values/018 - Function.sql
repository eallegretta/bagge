update [Function] set Action = 'L' where Action = 'R'
update [Function] set Action = 'G' where FullQualifiedName like '%Editor' and Action = 'L'


insert into [Function] (Name, FullQualifiedName, Action)
select 'Excepciones de Seguridad - Listar', 'Bagge.Seti.WebSite.SecurityExceptionList', 'L'
union
select 'Excepciones de Seguridad - Borrar', 'Bagge.Seti.WebSite.SecurityExceptionList', 'D'
union
select 'Excepciones de Seguridad - Ver',	'Bagge.Seti.WebSite.SecurityExceptionEditor', 'G'
union
select 'Excepciones de Seguridad - Crear',	'Bagge.Seti.WebSite.SecurityExceptionEditor', 'C'
union
select 'Excepciones de Seguridad - Editar', 'Bagge.Seti.WebSite.SecurityExceptionEditor', 'U'

