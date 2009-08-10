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
			Check.Require(user != null, Resources.InstanceCannotBeNull);

			if (user.IsSuperAdministrator)
				return true;


			function = Get(function.FullQualifiedName, Function.CharToAction(function.Action));

			if (function == null)
				return false;

			if (user.Functions.Contains(function))
				return true;

			return false;
		}

		public Function Get(string fullQualifiedName, FunctionAction action)
		{
			var filters = new List<FilterPropertyValue>();
			filters.Add("FullQualifiedName", fullQualifiedName);
			filters.Add("Action", Function.ActionToChar(action));

			var functions = _dao.FindAllByProperties(filters, null, null);

			if (functions.Length == 1)
				return functions[0];

			return null;
		}
	}
}
