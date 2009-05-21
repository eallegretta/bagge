IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ProductsByProviderReport')
	BEGIN
		DROP  Procedure  ProductsByProviderReport
	END

GO

create procedure ProductsByProviderReport
as
select	PROD.Id,
		PROD.Name,
		PROD.Description,
		min(PP.UnitaryPrice),
		PROV.Company

from  ProductProvider PP	inner join Product PROD on  PP.ProductId = PROD.Id
							inner join Provider PROV on PP.ProviderId = PROV.Id
where	PROD.Deleted = 0 and
		PROV.Deleted = 0 

GROUP BY	PROD.Id,
			PROD.Name,
			PROD.Description,
			PROV.Company
HAVING count(PP.UnitaryPrice) = 2
--order by P.Name asc

