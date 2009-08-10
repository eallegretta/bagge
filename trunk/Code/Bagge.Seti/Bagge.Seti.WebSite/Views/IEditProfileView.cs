using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.WebSite.Views
{
	public interface IEditProfileView : IEditorView<int>
	{
		string Password { get; }
		string ConfirmPassword { get; }
	}
}
