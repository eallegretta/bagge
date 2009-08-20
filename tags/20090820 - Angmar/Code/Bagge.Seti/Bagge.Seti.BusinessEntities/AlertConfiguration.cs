using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Securizable("Securizable_AlertConfiguration", typeof(AlertConfiguration))]
	public partial class AlertConfiguration: PrimaryKeyDomainObject<AlertConfiguration, int> 
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

		[Securizable("Securizable_AlertConfiguration_Days", typeof(AlertConfiguration))]
		[Property]
		[RangeValidator(0, RangeBoundaryType.Inclusive, 
			100, RangeBoundaryType.Ignore, 
			MessageTemplateResourceName = "Validators_AlertConfiguration_Days", 
			MessageTemplateResourceType = typeof(AlertConfiguration))]
		public int Days { get; set; }

		[Securizable("Securizable_AlertConfiguration_SendEmailToCreator", typeof(AlertConfiguration))]
		[Property]
		public bool SendEmailToCreator { get; set; }

		[Securizable("Securizable_AlertConfiguration_SendEmailToEmployees", typeof(AlertConfiguration))]
		[Property]
		public bool SendEmailToEmployees { get; set; }

		[Securizable("Securizable_AlertConfiguration_LastSentDate", typeof(AlertConfiguration))]
		[Property]
		public DateTime? LastSentDate { get; set; }

		[Securizable("Securizable_AlertConfiguration_MaxDaysPendingAproval", typeof(AlertConfiguration))]
		[Property]
		[RangeValidator(0, RangeBoundaryType.Inclusive,
			100, RangeBoundaryType.Ignore,
			MessageTemplateResourceName = "Validators_AlertConfiguration_MaxDaysPendingAproval",
			MessageTemplateResourceType = typeof(AlertConfiguration))]
		public int MaxDaysPendingAproval { get; set; }
	}
}
