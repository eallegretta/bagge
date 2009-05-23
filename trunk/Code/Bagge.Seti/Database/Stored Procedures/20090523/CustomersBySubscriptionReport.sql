USE [SETI]
GO
/****** Object:  StoredProcedure [dbo].[CustomersBySubscriptionReport]    Script Date: 05/23/2009 20:15:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomersBySubscriptionReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CustomersBySubscriptionReport]
GO
/****** Object:  StoredProcedure [dbo].[CustomersBySubscriptionReport]    Script Date: 05/23/2009 20:15:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomersBySubscriptionReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[CustomersBySubscriptionReport]

as

	
select  C.Name,
		C.CUIT,
		C.Address,
		C.Floor,
		C.Departament,
		C.ZipCode,
		C.City,
		C.Phone,
		C.MobilePhone,
		C.Email,
		C.Subscription

from Customer C    

where	C.Deleted = 0

order by C.Subscription,C.Name desc
' 
END
GO
