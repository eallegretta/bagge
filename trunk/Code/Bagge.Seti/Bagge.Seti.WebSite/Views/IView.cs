using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Bagge.Seti.WebSite.Views
{
	public interface IView
	{
		event EventHandler Init;
		event EventHandler Load;

		bool IsPostBack { get; }
		bool IsValid { get; }

		object DataSource { set; }
	}
}
