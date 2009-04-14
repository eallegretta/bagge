using System;
using System.Collections.Generic;
using System.Text;
using Castle.ActiveRecord;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public class District : PrimaryKeyWithNameDomainObject<District, int>, IComparable, IComparable<District>
	{
		[BelongsTo("CountryStateId")]
		public CountryState CountryState
		{
			get;
			set;
		}

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
