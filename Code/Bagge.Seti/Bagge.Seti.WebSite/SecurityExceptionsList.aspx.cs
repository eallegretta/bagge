using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.WebSite.Model;

namespace Bagge.Seti.WebSite
{
	public partial class SecurityExceptionsList : ListPage<SecurityException, int>
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override GridView Grid
		{
			get { return _securityExceptions; }
		}

		protected override Bagge.Seti.WebSite.Presenters.ListPresenter<SecurityException, int> Presenter
		{
			get { throw new NotImplementedException(); }
		}

		protected override Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}
	}
}
