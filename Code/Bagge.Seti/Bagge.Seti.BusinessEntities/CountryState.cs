using System;
using System.Collections.Generic;
using System.Text;

namespace Bagge.Seti.BusinessEntities
{
	public class CountryState : PrimaryKeyWithNameDomainObject<object, object>
	{
		public string ShortName
		{
			get; set;
		}

		public IList<District> Districts
		{
			get; set;
		}
	}
}
