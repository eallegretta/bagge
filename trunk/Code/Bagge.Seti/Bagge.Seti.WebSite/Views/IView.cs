using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.WebSite.Views
{
	public interface IView
	{
		event EventHandler Load;

		bool IsPostBack { get; }
		bool IsValid { get; }

		object DataSource { set; get; }
		void DataBind();
	}
}
