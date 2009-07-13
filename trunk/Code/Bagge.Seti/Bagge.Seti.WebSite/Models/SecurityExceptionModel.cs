using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.WebSite.Model
{
	public class SecurityExceptionModel
	{
		

		public SecurityExceptionModel(SecurityException securityException)
		{
			SecurityException = securityException;

			HumanReadableClassName = SecurizableAttribute.GetName(SecurityException.TargetType);
			HumanReadableMemberName = SecurizableAttribute.GetName(SecurityException.TargetType.GetMember(SecurityException.MemberName));
		}

		public SecurityException SecurityException
		{ 
			get; 
			private set; 
		}

		public string HumanReadableClassName
		{
			get;
			private set;
			
		}

		public string HumanReadableMemberName
		{
			get;
			private set;
		}

	}
}
