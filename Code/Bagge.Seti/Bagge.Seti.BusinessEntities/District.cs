using System;
using System.Collections.Generic;
using System.Text;

namespace Bagge.Seti.BusinessEntities
{
	public class District : PrimaryKeyWithNameDomainObject<object, object>
	{
		public CountryState CountryState
		{
			get;
			set;
		}

		public string ZipCode
		{
			get;
			set;
		}
	}
}
