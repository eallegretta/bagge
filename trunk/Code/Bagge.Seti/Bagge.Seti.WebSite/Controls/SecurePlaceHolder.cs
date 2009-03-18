using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecurePlaceHolder: PlaceHolder, ISecureControl
	{
		#region ISecureControl Members

		public string SecureTypeName
		{
			get; set;
		}

		public void ApplySecurityRestrictions(IList<Bagge.Seti.Security.BusinessEntities.Function> functions)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
