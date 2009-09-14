using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Views
{
	//Marker interface for siteMap
	public interface IEditorView: IView
	{
		EditorAction Mode { get; }

		object SelectedEntity { get; }

		event EventHandler DataBound;
	}

	public interface IEditorView<PK>: IEditorView
	{
		PK PrimaryKey { get; }
		byte[] Timestamp { get; set; }
	}
}
