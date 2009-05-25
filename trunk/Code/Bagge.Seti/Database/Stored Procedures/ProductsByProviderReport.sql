USE [SETI]
GO
/****** Object:  StoredProcedure [dbo].[ProductsByProviderReport]    Script Date: 05/25/2009 20:26:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductsByProviderReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProductsByProviderReport]
GO
/****** Object:  StoredProcedure [dbo].[ProductsByProviderReport]    Script Date: 05/25/2009 20:26:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ProductsByProviderReport]
as
select	PROD.Name 'Nombre',
		PROD.Description 'Descripcion',
		min(PP.UnitaryPrice) 'Min_Precio_Unitario',
		PROV.Company 'Compania'

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



