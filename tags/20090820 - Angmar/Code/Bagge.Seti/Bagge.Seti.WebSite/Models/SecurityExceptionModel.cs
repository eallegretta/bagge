using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Model
{
	public class SecurityExceptionViewModel: PrimaryKeyDomainObject<SecurityExceptionViewModel, int>
	{
		public SecurityExceptionViewModel(SecurityException securityException)
		{
		    SecurityException = securityException;

		    HumanReadableClassName = SecurizableAttribute.GetName(SecurityException.SecureEntity.TargetType);
		    HumanReadablePropertyName = SecurizableAttribute.GetName(SecurityException.SecureEntity.TargetType.GetProperty(SecurityException.PropertyName));
		}

		public override int Id
		{
			get
			{
				return SecurityException.Id;
			}
			set
			{
				SecurityException.Id = value;
			}
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

		public string HumanReadablePropertyName
		{
		    get;
		    private set;
		}

	}
}
