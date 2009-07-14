delete AccessibilityType

insert into AccessibilityType (Id, Name)
select 1, 'Sin Acceso'
union
select 2, 'Editar'
union
select 3, 'Ver'
