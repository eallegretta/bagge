﻿using System;
using System.Collections.Generic;
using System.Text;
using Castle.ActiveRecord;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class District : PrimaryKeyWithNameDomainObject<object, object>
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
	}
}