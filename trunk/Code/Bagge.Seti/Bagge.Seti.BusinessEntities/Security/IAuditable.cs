using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.Security.BusinessEntities
{
	public interface IAuditable
	{
		string AuditUserName { get; set; }
		byte[] AuditTimeStamp { get; set; }
		bool Deleted { get; set; }
	}
}
