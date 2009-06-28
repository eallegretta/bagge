using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.DataAccess.Contracts.Reports;
using Bagge.Seti.BusinessEntities;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace Bagge.Seti.DataAccess.Reports
{   
    public class RolesByUserReportDao: BaseReportDao<RolesByUserReport>, IReportDao
    {
        #region IReportDao Members

        public BaseReport GetReport(IList<FilterPropertyValue> filters)
        {
            var FilterName = filters.GetFilter("Name");
            var FilterDescription = filters.GetFilter("Description");

            string name = null;
            string description = null;

            if (FilterName != null)
            {
                name = FilterName.Value.ToString();
            }

            if (FilterDescription != null)
            {
                description = FilterDescription.Value.ToString();
            }

            return GetReport("RolesByUserReport", name, description);
        }

        #endregion
    }
}
