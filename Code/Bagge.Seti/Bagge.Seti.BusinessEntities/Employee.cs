using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Bagge.Seti.BusinessEntities.Properties;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;
using Castle.ActiveRecord;
using System;
using Bagge.Seti.BusinessEntities.Validators;
using System.Collections;

namespace Bagge.Seti.BusinessEntities
{
	[Serializable]
	[ActiveRecord]
	[Securizable("Securizable_Employee", typeof(Employee))]
	public partial class Employee : AuditablePrimaryKeyDomainObject<Employee, int>, IUser
	{
		[Property]
		[Securizable("Securizable_Employee_Username", typeof(Employee))]
		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Employee_Username", MessageTemplateResourceType = typeof(Employee))]
		public string Username
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Employee_Password", typeof(Employee))]
		public string Password
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Employee_FileNumber", typeof(Employee))]
		public string FileNumber
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Employee_Firstname", typeof(Employee))]
		public string Firstname
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Employee_Lastname", typeof(Employee))]
		public string Lastname
		{
			get;
			set;
		}

		[Securizable("Securizable_Employee_Fullname", typeof(Employee))]
		public string Fullname
		{
			get
			{
				return Settings.Default.EmployeeFullNameFormat.Replace("{Firstname}", Firstname).Replace("{Lastname}", Lastname);
			}
		}

		[Property]
		[Securizable("Securizable_Employee_Phone", typeof(Employee))]
		public string Phone
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Employee_EmergencyPhone", typeof(Employee))]
		public string EmergencyPhone
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Employee_Email", typeof(Employee))]
		[EmailValidator(MessageTemplateResourceName = "Validators_Employee_Email", MessageTemplateResourceType = typeof(Employee), Required = false)]
		public string Email
		{
			get;
			set;
		}


		[BelongsTo("CategoryId")]
		[Securizable("Securizable_Employee_Category", typeof(Employee))]
		public EmployeeCategory Category
		{
			get;
			set;
		}

		[Securizable("Securizable_Employee_Tickets", typeof(Employee))]
		[HasAndBelongsToMany(typeof(TicketEmployee), Table = "TicketEmployee", ColumnKey = "EmployeeId", ColumnRef = "TicketId", Lazy = true)]
		public IList<TicketEmployee> Tickets
		{
			get;
			set;
		}


		[Securizable("Securizable_Employee_Roles", typeof(Employee))]
		[HasAndBelongsToMany(Table = "RoleEmployee", ColumnKey = "EmployeeId", ColumnRef = "RoleId", Lazy = true, Inverse = false)]
		public virtual IList<Role> Roles
		{
			get;
			set;
		}

		IList<Function> _functions;

		public bool IsSuperAdministrator
		{
			get
			{
				var roles = Roles;
				if (roles == null || roles.Count == 0)
					return false;

				return (from role in roles
						where role.Id == Role.SuperAdministratorId
						select true).FirstOrDefault();
			}
		}

		public bool IsTechnician
		{
			get
			{
				return Category.Id == EmployeeCategory.TechnicianId;
			}
		}

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

		private IList<SecurityException> GetDistinctSecurityExceptions()
		{
			List<SecurityException> exceptions = new List<SecurityException>();
			foreach (var role in Roles)
			{
				var distinctExceptions = from exception in role.SecurityExceptions
										 where !exceptions.Contains(exception)
										 select exception;
				exceptions.AddRange(distinctExceptions.ToArray());
			}
			return exceptions;
		}

		[Property]
		public virtual string RecoverPasswordKey
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

		#region IUser Members


		IList<SecurityException> _securityExceptions;

		public IList<SecurityException> SecurityExceptions
		{
			get
			{
				if (_securityExceptions == null)
					_securityExceptions = GetDistinctSecurityExceptions();
				return _securityExceptions;
			}
		}

		#endregion
	}
}
