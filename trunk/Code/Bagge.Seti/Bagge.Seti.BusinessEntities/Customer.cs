﻿using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class Customer : AuditablePrimaryKeyWithNameDomainObject<Customer, int>
	{
		[Property]
		public string CUIT 
		{ 
			get; 
			set; 
		}


		[BelongsTo("DistrictId")]
		public District District
		{
			get;
			set;
		}
		
		[Property]
		public string Address
		{
			get;
			set;
		}

		[Property]
		public char Floor
		{
			get;
			set;
		}

		[Property("Departament")]
		public char Apartment
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

		[Property]
		public string City
		{
			get;
			set;
		}

		[Property]
		public string Phone
		{
			get;
			set;
		}

		[Property]
		public string MobilePhone
		{
			get;
			set;
		}

		[Property]
		public string Email
		{
			get;
			set;
		}

		[Property]
		public bool Subscription
		{
			get;
			set;
		}
	}
}
