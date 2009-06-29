using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.BusinessEntities
{
	public class FilterPropertyValue
	{

		public string Property
		{
			get;
			set;
		}

		public object Value
		{
			get;
			set;
		}

		public FilterPropertyValueType Type
		{
			get;
			set;
		}
	}
}
