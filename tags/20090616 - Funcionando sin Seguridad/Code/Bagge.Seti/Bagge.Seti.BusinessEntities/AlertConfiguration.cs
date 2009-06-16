using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class AlertConfiguration: PrimaryKeyDomainObject<AlertConfiguration, int> 
	{
		[PrimaryKey]
		public override int Id
		{
			get
			{
				return 1;
			}
			set
			{
				
			}
		}

		[Property]
		[RangeValidator(0, RangeBoundaryType.Inclusive, 
			100, RangeBoundaryType.Ignore, 
			MessageTemplateResourceName = "Validators_AlertConfiguration_Days", 
			MessageTemplateResourceType = typeof(AlertConfiguration))]
		public int Days { get; set; }
		
		[Property]
		public bool SendEmailToCreator { get; set; }
		
		[Property]
		public bool SendEmailToEmployees { get; set; }

		[Property]
		public DateTime? LastSentDate { get; set; }

		[Property]
		[RangeValidator(0, RangeBoundaryType.Inclusive,
			100, RangeBoundaryType.Ignore,
			MessageTemplateResourceName = "Validators_AlertConfiguration_MaxDaysPendingAproval",
			MessageTemplateResourceType = typeof(AlertConfiguration))]
		public int MaxDaysPendingAproval { get; set; }
	}
}
