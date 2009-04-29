delete ProviderCalification

 insert into ProviderCalification (Name)
 select 'Malo'
 union
 select 'Regular'
 union
 select 'Bueno'
 union
 select 'Muy Bueno'