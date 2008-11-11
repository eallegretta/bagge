using Bagge.Seti.BusinessEntities.Properties;
using System.Collections.Generic;

namespace Bagge.Seti.BusinessEntities
{
	public class Employee : PrimaryKeyDomainObject<Employee, int>
	{
		public string Username
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public string FileNumber
		{
			get;
			set;
		}

		public string Firstname
		{
			get;
			set;
		}

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

		public string Phone
		{
			get;
			set;
		}

		public string EmergencyPhone
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public EmployeeCategory Category
		{
			get;
			set;
		}

		public IList<TicketEmployee> Tickets
		{
			get;
			set;
		}
	}
}
