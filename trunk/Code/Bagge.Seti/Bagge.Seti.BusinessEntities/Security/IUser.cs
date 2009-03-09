using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessEntities.Security
{
	public interface IUser: IIdentity
	{
		string Username { get; set; }
		string Password { get; set; }
		IList<Role> Roles { get; set; }
		new bool IsAuthenticated { get; set; }
	}
}
