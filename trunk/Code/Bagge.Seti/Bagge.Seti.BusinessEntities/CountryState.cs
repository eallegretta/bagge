using System;
using System.Collections.Generic;
using System.Text;
using Castle.ActiveRecord;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class CountryState : PrimaryKeyWithNameDomainObject<CountryState, int>
	{
		[Property]
		public string ShortName
		{
			get;
			set;
		}

		[HasMany(Table = "District", Lazy = true, ColumnKey = "CountryStateId")]
		public IList<District> Districts
		{
			get;
			set;
		}
	}
}
