
/****** Object:  StoredProcedure [dbo].[ProductsByProviderReport]    Script Date: 05/25/2009 20:26:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductsByProviderReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProductsByProviderReport]
GO
/****** Object:  StoredProcedure [dbo].[ProductsByProviderReport]    Script Date: 05/25/2009 20:26:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* SELECCIONAR TODOS LOS INSUMOS DE MENOR PRECIO POR PROVEEDOR, ORDEN ASCENDENTE POR INSUMO. */
CREATE procedure [dbo].[ProductsByProviderReport]
@productName varchar(50),
@providerName varchar(50)
as
select	prod.Name 'Nombre',
		prod.Description 'Descripción', 
		pp.UnitaryPrice 'Precio',
		prov.Name 'Proveedor', 
		prov.Company 'Compañia'
from ProductProvider pp
	inner join (
					select ppi.ProductId, min
(ppi.UnitaryPrice) 'Price'
					from ProductProvider ppi
					group by ppi.ProductId) i 
	on pp.ProductId = i.ProductId and pp.UnitaryPrice = i.Price 
	inner join Product prod on pp.ProductId = prod.Id 
	inner join Provider prov on pp.ProviderId = prov.Id
where
	prod.Name like @productName and
	prov.Name like @providerName
order by pp.ProductId




