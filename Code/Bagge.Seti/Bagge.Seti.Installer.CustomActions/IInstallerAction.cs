using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace Bagge.Seti.Installer.CustomActions
{
	public interface IInstallerAction
	{
		void Install(IDictionary<string, object> parameters);
		void Uninstall(IDictionary<string, object> parameters);
	}
}
