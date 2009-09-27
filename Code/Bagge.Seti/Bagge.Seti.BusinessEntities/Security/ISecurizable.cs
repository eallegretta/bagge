using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.Constraints;

namespace Bagge.Seti.Security.BusinessEntities
{
	public interface ISecurizable
	{
		bool IsAccessible { get; set; }
	}
}
