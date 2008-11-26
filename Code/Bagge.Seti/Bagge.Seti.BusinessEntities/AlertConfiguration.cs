using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class AlertConfiguration
	{
		[Property]
		public int Days { get; set; }
		
		[Property]
		public bool SendEmailToCreator { get; set; }
		
		[Property]
		public bool SendEmailToEmployees { get; set; }
	}
}
