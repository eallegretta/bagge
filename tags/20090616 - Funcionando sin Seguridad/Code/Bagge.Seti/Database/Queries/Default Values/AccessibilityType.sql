delete AccessibilityType

insert into AccessibilityType (Id, Name)
select 1, 'None'
union
select 2, 'Edit'
union
select 3, 'View'
union
select 4, 'Execute' 