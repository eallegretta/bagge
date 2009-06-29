using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.DataAccess;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessLogic
{
	[Securizable("Securizable_RoleManager", typeof(RoleManager))]
	public partial class RoleManager : AuditableGenericManager<Role, int>, IRoleManager
	{
		IEmployeeDao _employeeDao;
		IFunctionDao _functionDao;

		public RoleManager(IRoleDao dao, IEmployeeDao employeeDao, IFunctionDao functionDao)
			: base(dao)
		{
			_employeeDao = employeeDao;
			_functionDao = functionDao;
		}

		protected override void ReplaceFilters(IList<FilterPropertyValue> filters)
		{
			var employeeFilter = (from filter in filters
								  where filter.Property == "Employees" && filter.Value is int
								  select filter).FirstOrDefault();

			if (employeeFilter != null)
				employeeFilter.Value = _employeeDao.Get((int)employeeFilter.Value);

			var functionFilter = (from filter in filters
								  where filter.Property == "Functions" && filter.Value is int
								  select filter).FirstOrDefault();

			if (functionFilter != null)
				functionFilter.Value = _functionDao.Get((int)functionFilter.Value);
		}

		public override void Update(Role instance)
		{
			if (!IsDeleteOrUndelete)
				SessionScopeUtils.FlushSessionScope();

			base.Update(instance);
		}

	}
}
