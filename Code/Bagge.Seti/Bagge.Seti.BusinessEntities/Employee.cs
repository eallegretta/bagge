using Bagge.Seti.BusinessEntities.Properties;
using System.Collections.Generic;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using System.Security.Principal;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class Employee : AuditablePrimaryKeyDomainObject<Employee, int>, IUser
	{
		[Property]
		public string Username
		{
			get;
			set;
		}

		[Property]
		public string Password
		{
			get;
			set;
		}

		[Property]
		public string FileNumber
		{
			get;
			set;
		}

		[Property]
		public string Firstname
		{
			get;
			set;
		}

		[Property]
		public string Lastname
		{
			get;
			set;
		}

		public string Fullname
		{
			get
			{
				return Settings.Default.EmployeeFullNameFormat.Replace("{Firstname}", Firstname).Replace("{Lastname}", Lastname);
			}
		}

		[Property]
		public string Phone
		{
			get;
			set;
		}

		[Property]
		public string EmergencyPhone
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

		[BelongsTo("CategoryId")]
		public EmployeeCategory Category
		{
			get;
			set;
		}

		[HasAndBelongsToMany(typeof(TicketEmployee), Table = "TicketEmployee", ColumnKey = "EmployeeId", ColumnRef = "TicketId", Lazy = true)]
		public IList<TicketEmployee> Tickets
		{
			get;
			set;
		}


		[HasAndBelongsToMany(Table = "RoleEmployee", ColumnKey = "EmployeeId", ColumnRef = "RoleId", Lazy = true, Inverse = true)]
		public virtual IList<Role> Roles
		{
			get;
			set;
		}

		#region IIdentity Members

		public string AuthenticationType
		{
			get { return "SetiAuthentication"; }
		}

		public bool IsAuthenticated
		{
			get;
			set;
		}

		public string Name
		{
			get 
			{
				if (IsAuthenticated)
					return Username;
				return string.Empty;
			}
		}

		#endregion
	}
}
