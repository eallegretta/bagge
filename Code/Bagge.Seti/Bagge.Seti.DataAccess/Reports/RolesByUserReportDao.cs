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
            var filterName = filters.GetFilter("Name");
            var filterDescription = filters.GetFilter("Description");

            SqlString name = filterName.ToString();
            SqlString description = filterDescription.ToString();


            return GetReport("RolesByUserReport", name, description);
        }

        #endregion
    }
}
