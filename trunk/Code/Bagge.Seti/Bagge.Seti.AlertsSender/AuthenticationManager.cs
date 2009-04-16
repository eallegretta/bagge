using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Common;
using System.Threading;

namespace Bagge.Seti.AlertsSender
{
	public class AuthenticationManager: IAuthenticator
	{
		#region IAuthenticator Members

		public System.Security.Principal.IPrincipal LoggedInUser
		{
			get
			{
				return Thread.CurrentPrincipal;
			}
			set
			{
				Thread.CurrentPrincipal = value;
			}
		}

		#endregion
	}
}
