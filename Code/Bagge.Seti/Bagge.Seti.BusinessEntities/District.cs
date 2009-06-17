using System;
using System.Collections.Generic;
using System.Text;
using Castle.ActiveRecord;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	[Securizable("Securizable_District", typeof(District))]
	public partial class District : PrimaryKeyWithNameDomainObject<District, int>, IComparable, IComparable<District>
	{
		[Securizable("Securizable_District_CountryState", typeof(District))]
		[BelongsTo("CountryStateId")]
		public CountryState CountryState
		{
			get;
			set;
		}

		[Securizable("Securizable_District_ZipCode", typeof(District))]
		[Property]
		public string ZipCode
		{
			get;
			set;
		}

		#region IComparable<District> Members

		public int CompareTo(District other)
		{
			return Name.CompareTo(other.Name);
		}

		#endregion

		#region IComparable Members

		int IComparable.CompareTo(object obj)
		{
			return CompareTo((District)obj);
		}

		#endregion
	}
}
