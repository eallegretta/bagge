using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Bagge.Seti.Common
{
	public interface IAuthenticator
	{
		IPrincipal LoggedInUser { get; set; }
	}
}
