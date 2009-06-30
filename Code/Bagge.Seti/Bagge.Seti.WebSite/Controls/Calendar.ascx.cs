using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Threading;
using System.ComponentModel;
using Bagge.Seti.WebSite.Helpers;

namespace Bagge.Seti.WebSite.Controls
{
	[ValidationProperty("Date")]
	public partial class Calendar : System.Web.UI.UserControl
	{
		public string ValidationGroup
		{
			get { return _calendarReqVal.ValidationGroup; }
			set { _calendarReqVal.ValidationGroup = value; }
		}

		public string RequiredErrorMessage
		{
			get { return _calendarReqVal.ErrorMessage; }
			set { _calendarReqVal.ErrorMessage = value; }
		}

		public string CompareErrorMessage
		{
			get { return _calendarCompare.ErrorMessage; }
			set { _calendarCompare.ErrorMessage = value; }
		}

		public bool IsRequired
		{
			get { return _calendarReqVal.Visible; }
			set { _calendarReqVal.Visible = value; }
		}

		public string CompareToCalendarId
		{
			get
			{
				return ViewState["CompareToCalendarId"] as string;
			}
			set
			{
				ViewState["CompareToCalendarId"] = value;
			}
		}

		public ValidationCompareOperator CompareOperator
		{
			get { return _calendarCompare.Operator; }
			set
			{
				if (value == ValidationCompareOperator.DataTypeCheck)
					throw new ArgumentOutOfRangeException();
				_calendarCompare.Operator = value;
			}
		}



		public bool ShowTime
		{
			get { return _timePlaceHolder.Visible; }
			set { _timePlaceHolder.Visible = value; }
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			//if (!IsPostBack)
			//	_calendar.Text = DateTime.Now.ToShortDateString();
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			//SetCalendarComparesTo();
			RegisterJavascript();
			_calendarExt.Format = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
			if (IsPostBack)
			{

				string date = Request.Form[_calendar.UniqueID];
				if (!string.IsNullOrEmpty(date))
				{
					if (_calendar.Text != date)
					{
						_calendar.Text = date;
						OnDateChanged(sender, e);
					}
				}
			}
			//else
			//	_calendarExt.Format = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;

		}

		private void SetCalendarComparesTo()
		{
			if (!string.IsNullOrEmpty(CompareToCalendarId))
			{
				_calendarCompare.Visible = true;
				_calendarCompare.ControlToCompare = CompareToCalendarId;
			}
			else
				_calendarCompare.Visible = false;
		}

		private void RegisterJavascript()
		{
			string script = string.Format("var {0} = new Controls_Calendar('{1}','{2}','{3}');{4}", ID, _calendar.ClientID, _calendarImage.ClientID, _calendarImageDisabled.ClientID, Environment.NewLine);
			if (!Page.ClientScript.IsClientScriptIncludeRegistered("Calendar"))
			{
				string appPath = Request.ApplicationPath;
				if (!appPath.EndsWith("/"))
					appPath += "/";
				Page.ClientScript.RegisterClientScriptInclude("Calendar", appPath + "Controls/Calendar.js");
			}
			Page.ClientScript.RegisterStartupScript(typeof(string), "Calendar_" + ID, script, true);
		}
		protected void OnDateChanged(object sender, EventArgs e)
		{
			if (_dateChanged != null)
				_dateChanged(sender, e);
		}

		private EventHandler _dateChanged;

		public event EventHandler DateChanged
		{
			add
			{
				_dateChanged += value;
			}
			remove
			{
				_dateChanged -= value;
			}
		}

		public bool AutoPostBack
		{
			get { return _calendar.AutoPostBack; }
			set { _calendar.AutoPostBack = value; }
		}

		public string OnClientDateSelectionChanged
		{
			get { return _calendarExt.OnClientDateSelectionChanged; }
			set { _calendarExt.OnClientDateSelectionChanged = value; }
		}

		public bool Enabled
		{
			get
			{
				object enabled = ViewState["Enabled"];
				if (enabled == null)
					return true;
				return (bool)enabled;
			}
			set
			{
				ViewState["Enabled"] = value;
				_calendarExt.Enabled = value;
				if (value)
				{
					_calendarImage.Style["display"] = "";
					_calendarImageDisabled.Style["display"] = "none";
				}
				else
				{
					_calendarImage.Style["display"] = "none";
					_calendarImageDisabled.Style["display"] = "";
				}
				_calendar.Enabled = value;
			}

		}

		[Bindable(true, BindingDirection.TwoWay)]
		[TypeConverter(typeof(DateTimeConverter))]
		public DateTime NonNullableDate
		{
			get
			{
				return (Date.HasValue) ? Date.Value : DateTime.MinValue;
			}
			set
			{
				if (value == DateTime.MinValue)
					Date = null;
				else
					Date = value;
			}
		}

		[Bindable(true, BindingDirection.TwoWay)]
		[TypeConverter(typeof(DateTimeConverter))]
		public DateTime? Date
		{
			get
			{
				string date = _calendar.Text.Trim();
				if (string.IsNullOrEmpty(date))
					return null;
				return DateTime.Parse(date);
			}
			set { _calendar.Text = (value.HasValue) ? value.Value.ToShortDateString() : string.Empty; }
		}
	}
}
