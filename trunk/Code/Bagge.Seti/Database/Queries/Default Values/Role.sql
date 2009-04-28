 set identity_insert Role on
 
 insert into Role (Id, Name)
 select 1, 'Super Administrador'
 set identity_insert Role off
