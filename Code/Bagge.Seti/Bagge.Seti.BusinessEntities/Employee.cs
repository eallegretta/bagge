using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Bagge.Seti.BusinessEntities.Properties;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;
using Castle.ActiveRecord;
using System;

namespace Bagge.Seti.BusinessEntities
{
	[Serializable]
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

		IList<Function> _functions; 

		public IList<Function> Functions
		{
			get
			{
				if (_functions == null)
					_functions = GetDistinctFunctions();
				return _functions;
			}
		}

		private IList<Function> GetDistinctFunctions()
		{
			List<Function> functions = new List<Function>();
			foreach (var role in Roles)
			{
				var distinctFunctions = from function in role.Functions
										where !functions.Contains(function)
										select function;
				functions.AddRange(distinctFunctions.ToArray());
			}
			return functions;
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
