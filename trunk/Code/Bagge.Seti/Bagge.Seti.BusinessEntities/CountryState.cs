using System;
using System.Collections.Generic;
using System.Text;
using Castle.ActiveRecord;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	[Securizable("Securizable_CountryState", typeof(CountryState))]
	public partial class CountryState : PrimaryKeyWithNameDomainObject<CountryState, int>
	{
		[Property]
		[Securizable("Securizable_CountryState_ShortName", typeof(CountryState))]
		public string ShortName
		{
			get;
			set;
		}

		[Securizable("Securizable_CountryState_Districts", typeof(CountryState))]
		[HasMany(Table = "District", Lazy = true, ColumnKey = "CountryStateId", OrderBy = "Name")]
		public IList<District> Districts
		{
			get;
			set;
		}
	}
}
