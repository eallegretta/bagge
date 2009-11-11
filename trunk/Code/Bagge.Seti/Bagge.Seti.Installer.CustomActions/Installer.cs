using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Diagnostics;


namespace Bagge.Seti.Installer.CustomActions
{
	[RunInstaller(true)]
	public partial class Installer : System.Configuration.Install.Installer
	{
		private CustomAction[] _actions = new CustomAction[] { };

		public Installer()
		{
			InitializeComponent();

		}

		public override void Install(IDictionary stateSaver)
		{
#if DEBUG
			Debugger.Break();
#endif
			base.Install(stateSaver);

			foreach (string key in Context.Parameters.Keys)
			{
				stateSaver.Add(key, Context.Parameters[key]);
			}

			foreach (var action in _actions)
				action.Install(stateSaver);
		}

		public override void Rollback(IDictionary savedState)
		{
			base.Rollback(savedState);

			foreach (var action in _actions)
				action.Rollback(savedState);
		}

		public override void Commit(IDictionary savedState)
		{
			base.Commit(savedState);

			foreach (var action in _actions)
				action.Commit(savedState);
		}

		public override void Uninstall(IDictionary savedState)
		{
			base.Uninstall(savedState);

			foreach (var action in _actions)
				action.Commit(savedState);
		}
	}
}
