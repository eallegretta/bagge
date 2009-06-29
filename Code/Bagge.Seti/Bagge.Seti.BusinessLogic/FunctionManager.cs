using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;
using System.Reflection;
using System.IO;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.BusinessLogic.Properties;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic
{
	[Securizable("Securizable_FunctionManager", typeof(FunctionManager))]
	public class FunctionManager : IFunctionManager
	{
		IFunctionDao _dao;

		public FunctionManager(IFunctionDao dao)
		{
			_dao = dao;
		}

		public Function[] FindAll()
		{
			return _dao.FindAll("Name", true);
		}

		public Function Get(int id)
		{
			return _dao.Get(id);
		}


		public bool UserHasAccessToFunction(IUser user, Function function)
		{
			try
			{
				Check.Require(user != null, Resources.InstanceCannotBeNull);

				if (user.IsSuperAdministrator)
					return true;

				var filters = new List<FilterPropertyValue>();
				filters.Add("FullQualifiedName", function.FullQualifiedName);
				filters.Add("Action", function.Action);

				function = _dao.FindAllByProperties(filters, null, null)[0];

				if (user.Functions.Contains(function))
				{
					user.CurrentFunction = function;
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
