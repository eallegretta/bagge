using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Controls
{
	public interface ISecureControl
	{
		string SecureTypeName { get; set; }
		void ApplySecurityRestrictions(IList<Function> functions);
	}
}
