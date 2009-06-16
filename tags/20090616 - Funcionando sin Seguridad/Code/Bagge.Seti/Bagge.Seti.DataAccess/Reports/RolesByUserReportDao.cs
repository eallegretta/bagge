using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.DataAccess.Contracts.Reports;
using Bagge.Seti.BusinessEntities;
using System.Data.SqlTypes;

namespace Bagge.Seti.DataAccess.Reports
{   
    public class RolesByUserReportDao: BaseReportDao<RolesByUserReport>, IReportDao
    {
        #region IReportDao Members

        public BaseReport GetReport(IList<FilterPropertyValue> filters)
        {
            var name = filters.GetFilter("Name");
            var description = filters.GetFilter("Description");

            return GetReport("RolesByUserReport", name, description);
        }

        #endregion
    }
}
