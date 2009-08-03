using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagedFusion.Rewriter.Configuration;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Bagge.Seti.WebSite
{
	public static class LinkHelper
	{

		private static Dictionary<string, string> _links;

		public static Dictionary<string, string> Links
		{
			get
			{
				if (_links == null)
					_links = GetLinks();
				return _links;
			}
		}

		private static Dictionary<string, string> GetLinks()
		{
			ManagedFusionRewriterSectionGroup sectionGroup = ConfigurationManager.GetSection("managedFusion.rewriter") as ManagedFusionRewriterSectionGroup;
			if(sectionGroup != null)
			{
				var section = sectionGroup.Rules.Apache;
				
				Dictionary<string, string> links = new Dictionary<string, string>();

				var lines = File.ReadAllLines(HttpContext.Current.Server.MapPath(section.DefaultFileName));

				var startIndex = IndexOfCurrentUICulture(lines, Thread.CurrentThread.CurrentUICulture.Name);
				if (startIndex == -1)
					startIndex = IndexOfCurrentUICulture(lines, "Default");

				for (int index = startIndex + 1; index < lines.Length; index++)
				{
					string line = lines[index];
					if (line.ToUpperInvariant().StartsWith("# ENDCULTURE"))
						break;

					if (line.StartsWith("#"))
						continue;

					string[] pair = GetLink(line);
					if(pair != null)
						links.Add(pair[1], pair[0]);
				}
				return links;
			}
			return new Dictionary<string,string>();
		}

		private static string[] GetLink(string line)
		{
			string[] splittedLine = line.Split(' ','\t');
			string[] urls = (from l in splittedLine
							where l.StartsWith("^/") || l.StartsWith("/")
							select l).ToArray();

			if (urls.Length == 2)
			{
				urls[0] = "~" + urls[0].Substring(1);
				urls[1] = "~" + urls[1];
				return urls;
			}
			return null;
		}

		public static string Link(string url)
		{
			if (url.StartsWith("/"))
				url = "~" + url;
			else if (!url.StartsWith("~/"))
				url = "~/" + url;

			if(Links.ContainsKey(url))
				return new Page().ResolveUrl(Links[url].Replace("\\.aspx$", ".aspx"));

			return string.Empty;
		}

		public static string EditLink(string url, string id)
		{
			url += "?Id=$1";
			if(Links.ContainsKey(url))
				return Links[url].Replace("(\\d+)", id).Replace("\\.aspx$", ".aspx");

			return string.Empty;
		}

		public static string ViewLink(string url, string id)
		{
			url += "?Id=$1&Action=View";
			if(Links.ContainsKey(url))
				return Links[url].Replace("(\\d+)", id).Replace("\\.aspx$", ".aspx");

			return string.Empty;
		}

		private static int IndexOfCurrentUICulture(string[] lines, string cultureName)
		{
			for (int index = 0; index < lines.Length; index++)
			{
				if(lines[index].StartsWith("# CULTURE: " + cultureName, StringComparison.InvariantCultureIgnoreCase))
					return index;
			}
			return -1;
		}
	}
}
