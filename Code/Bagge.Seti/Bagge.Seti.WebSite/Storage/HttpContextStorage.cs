using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.Common;
using System.Threading;
using System.Web.SessionState;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Storage
{
	public class HttpContextStorage: IStorage
	{
		#region IContextStorage Members

		public void SetData(object key, object value)
		{
			HttpContext.Current.Items[key] = value;
		}

		public object GetData(object key)
		{
			return HttpContext.Current.Items[key];
		}

		#endregion
	}
}
