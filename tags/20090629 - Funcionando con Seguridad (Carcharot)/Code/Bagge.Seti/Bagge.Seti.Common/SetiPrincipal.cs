using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.Common
{
	public class SetiPrincipal: IPrincipal
	{
		#region IPrincipal Members

		private IUser _user; 

		public SetiPrincipal(IUser user)
		{
			_user = user;
		}

		public IIdentity Identity
		{
			get { return _user; }
		}

		public bool IsInRole(string roleName)
		{
			foreach (Role role in _user.Roles)
			{
				if (role.Name.Equals(roleName, StringComparison.InvariantCultureIgnoreCase))
					return true;
			}
			return false;
		}

		#endregion
	}
}
