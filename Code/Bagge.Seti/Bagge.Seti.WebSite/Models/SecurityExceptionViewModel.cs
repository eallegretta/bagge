using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Model
{
	public class SecurityExceptionViewModel: PrimaryKeyDomainObject<SecurityExceptionViewModel, int>
	{
		public SecurityExceptionViewModel()
		{
		}

		public SecurityExceptionViewModel(SecurityException securityException)
		{
		    this.SecurityException = securityException;

		    HumanReadableClassName = SecurizableAttribute.GetName(SecurityException.SecureEntity.TargetType);
		    HumanReadablePropertyName = SecurizableAttribute.GetName(SecurityException.SecureEntity.TargetType.GetProperty(SecurityException.PropertyName));
			Constraint = Bagge.Seti.Security.Constraints.Constraint.GetConstraintName(SecurityException.ConstraintType);
		}

		public override int Id
		{
			get
			{
				if(this.SecurityException != null)
					return this.SecurityException.Id;
				return 0;
			}
			set
			{
				if(this.SecurityException != null)
					this.SecurityException.Id = value;
			}
		}

		public int SecureEntityId
		{
			get { return this.SecurityException.SecureEntity.Id; }
		}

		public Function Function
		{
			get { return this.SecurityException.SecureEntity.Function; }
		}

		public Role Role
		{
			get { return this.SecurityException.Role; }
		}

		private SecurityException SecurityException
		{ 
		    get; 
		    set; 
		}

		public string PropertyName
		{
			get { return SecurityException.PropertyName; }
		}

		public string HumanReadableClassName
		{
		    get;
		    private set;
		}

		public string ClassFullQualifiedName
		{
			get
			{
				return SecurityException.SecureEntity.ClassFullQualifiedName;
			}
		}

		public string HumanReadablePropertyName
		{
		    get;
		    private set;
		}


		public string Constraint
		{
			get;
			private set;
		}

		public string ConstraintType
		{
			get
			{
				return SecurityException.ConstraintType;
			}
			set { SecurityException.ConstraintType = value; }
		}

		public object Value
		{
			get
			{
				return SecurityException.Value;
			}
			set { SecurityException.Value = value; }
		}

	}
}
