using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace Bagge.Seti.Installer.CustomActions
{
	public abstract class CustomAction
	{
		public virtual void Install(IDictionary stateSaver)
		{
		}

		public virtual void Commit(IDictionary savedState)
		{
		}

		public virtual void Rollback(IDictionary savedState)
		{
		}

		public virtual void Uninstall(IDictionary savedState)
		{
		}
	}
}
