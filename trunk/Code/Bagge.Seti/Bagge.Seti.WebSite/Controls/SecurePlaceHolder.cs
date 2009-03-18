using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.Helpers;

namespace Bagge.Seti.WebSite.Controls
{
	public abstract class SecurePlaceHolder : PlaceHolder, ISecureControl
	{
		public string SecureTypeName
		{
			get;
			set;
		}

		public abstract void ApplySecurityRestrictions(IList<Bagge.Seti.Security.BusinessEntities.Function> functions);
		
	}
}
