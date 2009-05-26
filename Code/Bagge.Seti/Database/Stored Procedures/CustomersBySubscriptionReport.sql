USE [SETI]
GO
/****** Object:  StoredProcedure [dbo].[CustomersBySubscriptionReport]    Script Date: 05/25/2009 20:21:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomersBySubscriptionReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CustomersBySubscriptionReport]
GO
/****** Object:  StoredProcedure [dbo].[CustomersBySubscriptionReport]    Script Date: 05/25/2009 20:21:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* SELECCIONAR TODOS LOS CLIENTES C/ABONO Y  S/ABONO. ORDEN POR ABONO DESC. */
CREATE procedure [dbo].[CustomersBySubscriptionReport]

as

	
select  C.Name 'Nombre',
		C.CUIT 'CUIT',
		C.Address 'Dirección',
		C.Floor 'Piso',
		C.Departament 'Deptartamento',
		C.ZipCode 'Código Postal',
		C.City 'Ciudad',
		C.Phone 'Teléfono',
		C.MobilePhone 'Celular',
		C.Email 'E-Mail',
		C.Subscription 'Abonado'

from Customer C    

where	C.Deleted = 0

order by C.Subscription,C.Name desc


