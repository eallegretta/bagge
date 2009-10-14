using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Bagge.Seti.Installer.CustomActions
{
	public static class ContentHelper
	{
		public static string GetContent(string name)
		{
			using (var sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(name)))
				return sr.ReadToEnd();
		}
	}
}
