using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;
using System.Globalization;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite
{
	 

	[SecurizableCrud("Securizable_Default", typeof(_Default), FunctionAction.Retrieve)]
	public partial class _Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			BindDayNames();
			BindDays();
		}


		private DateTime? CurrentDayOfWeek
		{
			get
			{
				var currentDayOfWeek = Request.QueryString["CurrentDayOfWeek"];
				DateTime date;
				if (DateTime.TryParse(currentDayOfWeek, out date))
					return date;
				return null;
			}
		}

		private void BindDays()
		{
			DayOfWeek dayOfWeek;
			DateTime start;
			if (CurrentDayOfWeek.HasValue)
			{
				dayOfWeek = CurrentDayOfWeek.Value.DayOfWeek;
				start = CurrentDayOfWeek.Value;
			}
			else
			{
				dayOfWeek = DateTime.Now.DayOfWeek;
				start = DateTime.Now;
			}
			int days = dayOfWeek - DayOfWeek.Sunday;
			start = start.AddDays(-days);
			DateTime[] week = new DateTime[7];
			for(int day = 0; day < 7; day++)
				week[day] = start.AddDays(day);
			_details.DataSource = _days.DataSource = week;
			_days.DataBind();
			_details.DataBind();
			_prevWeek.NavigateUrl = "~/Default.aspx?CurrentDayOfWeek=" + start.AddDays(-7).ToShortDateString().Replace("/", "-");
			_nextWeek.NavigateUrl = "~/Default.aspx?CurrentDayOfWeek=" + start.AddDays(7).ToShortDateString().Replace("/","-");
		}

		protected void _details_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var user = IoCContainer.User.Identity as IUser;
				var date = (DateTime)e.Item.DataItem;
				var tickets = (Repeater)e.Item.FindControl("_tickets");
				if (user.IsSuperAdministrator)
					tickets.DataSource = IoCContainer.TicketManager.FindAllByExecutionDate(date);
				else
					tickets.DataSource = IoCContainer.TicketManager.FindAllByExecutionDateAndTechnician(date, user.Id);
				tickets.DataBind();
			}
		}

		private void BindDayNames()
		{
			_dayNames.DataSource = CultureInfo.CurrentUICulture.DateTimeFormat.DayNames;
			_dayNames.DataBind();
		}
	}
}
