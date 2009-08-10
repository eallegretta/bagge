using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite
{
	[SecurizableCrud("Securizable_Default", typeof(_Default), FunctionAction.Retrieve)]
	public partial class _Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}
