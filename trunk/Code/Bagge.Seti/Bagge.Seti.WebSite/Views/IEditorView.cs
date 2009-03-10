using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Views
{
	public interface IEditorView<PK>: IView
	{
		PK PrimaryKey { get; }
		EditorAction Mode { get; }

		event EventHandler DataBound;
	}
}
