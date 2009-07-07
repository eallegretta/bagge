using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.BusinessEntities
{
	public enum FilterPropertyValueType
	{
		Equals,
		NotEquals,
		Like,
		NotLike,
		Contains,
		NotContains,
		Greater,
		GreaterEquals,
		Lower,
		LowerEquals,
		In,
		NotIn
	}
}
