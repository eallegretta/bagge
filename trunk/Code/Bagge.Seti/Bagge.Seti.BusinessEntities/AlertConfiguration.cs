using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace Bagge.Seti.BusinessEntities
{
	public class AlertConfiguration
	{
		public int Days { get; set; }
		
		public bool SendEmailToCreator { get; set; }
		
		public bool SendEmailToEmployees { get; set; }
	}
}
