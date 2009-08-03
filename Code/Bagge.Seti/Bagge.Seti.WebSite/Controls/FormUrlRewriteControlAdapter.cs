using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.Adapters;

namespace Bagge.Seti.WebSite.Controls
{
	public class FormUrlRewriteControlAdapter : ControlAdapter
	{
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(new RewriteFormHtmlTextWriter(writer));
		}


	}

	internal class RewriteFormHtmlTextWriter : HtmlTextWriter
	{
		public RewriteFormHtmlTextWriter(HtmlTextWriter writer)
			: base(writer)
		{
			InnerWriter = writer.InnerWriter;
		}
		public RewriteFormHtmlTextWriter(TextWriter writer)
			: base(writer)
		{
			InnerWriter = writer;
		}

		public override void WriteAttribute(string name, string value, bool fEncode)
		{
			//If the attribute we are writing is the "action" attribute, and we are not on a sub-control, 
			//then replace the value to write with the raw URL of the request - which ensures that we'll
			//preserve the PathInfo value on postback scenarios
			if (name == "action")
			{
				var context = HttpContext.Current;
				if (context.Items["ActionAlreadyWritten"] == null)
				{
					//Because we are using the UrlRewriting.net HttpModule, we will use the 
					//Request.RawUrl property within ASP.NET to retrieve the origional URL
					//before it was re-written.  You'll want to change the line of code below
					//if you use a different URL rewriting implementation.
					value = context.Request.RawUrl;
					context.Items.Add("ActionAlreadyWritten", true);
				}
			}
			base.WriteAttribute(name, value, fEncode);
		}
	}
}
