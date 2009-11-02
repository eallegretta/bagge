using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Castle.ActiveRecord.Framework;
using System.Reflection;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using System.Globalization;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Net;

namespace Bagge.Seti.WebSite
{
	public class Global : System.Web.HttpApplication
	{
		private class BaggeCulture : CultureInfo
		{

			public static BaggeCulture Instance = new BaggeCulture();

			NumberFormatInfo _numberFormatInfo;

			public BaggeCulture()
				: base("es-AR")
			{
				_numberFormatInfo = new NumberFormatInfo();
				base.NumberFormat.CopyTo<NumberFormatInfo>(_numberFormatInfo);
				_numberFormatInfo.CurrencyDecimalSeparator = ".";
				_numberFormatInfo.CurrencyGroupSeparator = ",";
				_numberFormatInfo.NumberDecimalSeparator = ".";
				_numberFormatInfo.NumberGroupSeparator = ",";
			}

			public override NumberFormatInfo NumberFormat
			{
				get
				{
					return _numberFormatInfo;
				}
				set
				{
					_numberFormatInfo = value;
				}
			}

			public override string EnglishName
			{
				get
				{
					return "Spanish (Bagge Custom)";
				}
			}

			public override string Name
			{
				get
				{
					return "es-AR-Bagge";
				}
			}
		}


		protected void Application_Start(object sender, EventArgs e)
		{
			IConfigurationSource config = ActiveRecordSectionHandler.Instance;
			Assembly asm = Assembly.Load("Bagge.Seti.BusinessEntities");
			ActiveRecordStarter.Initialize(asm, config);

			
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			if (Thread.CurrentThread.CurrentCulture.Name == "es-AR")
				Thread.CurrentThread.CurrentCulture = BaggeCulture.Instance;
			if (Thread.CurrentThread.CurrentUICulture.Name == "es-AR")
				Thread.CurrentThread.CurrentUICulture = BaggeCulture.Instance;
		}

		protected void Application_EndRequest(object sender, EventArgs e)
		{
			var context = HttpContext.Current;
			if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
			{
				Response.Redirect("~/NotAuthorized.aspx");
			}
		}


		private string GetSerializedException(Exception ex)
		{
			XmlSerializer ser = new XmlSerializer(typeof(Exception));
			using (var writer = new StringWriter())
			{
				ser.Serialize(writer, ex);
				return writer.ToString();
			}
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Context.Items["LastError"] = Server.GetLastError();
			Server.Transfer("~/Error.aspx");
		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}