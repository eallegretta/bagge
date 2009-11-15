using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Helpers;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Reports
{
	[SecurizableCrud("Securizable_RolesByUser", typeof(RolesByUser), FunctionAction.List)]
    public partial class RolesByUser : FilteredReportPage<RolesByUserReport>
    {
        protected override Button FilterButton
        {
            get { return _filter; }
        }
        public override IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters
        {
            get
            {
                var filters = new List<FilterPropertyValue>();
                FilterHelper.AddTextBoxFilterValue<string>(_name, "Name", FilterPropertyValueType.Like, filters);
                FilterHelper.AddTextBoxFilterValue<string>(_description, "Description", FilterPropertyValueType.Like, filters);
                FilterHelper.AddTextBoxFilterValue<string>(_userName, "UserName", FilterPropertyValueType.Like, filters);

                return filters;
            }
        }
    }
}

