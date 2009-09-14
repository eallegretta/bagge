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
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.BusinessEntities;
using System.Web.UI.HtmlControls;
using Bagge.Seti.WebSite.Models;

namespace Bagge.Seti.WebSite
{
	 

	[SecurizableCrud("Securizable_Default", typeof(_Default), FunctionAction.List)]
	public partial class _Default : Page, IHomeView
	{
		HomePresenter _presenter;
		bool _canViewTicket, _canUpdateTicket, _canUpdateProgressTicket;
		public _Default()
		{


			_presenter = new HomePresenter(this, IoCContainer.TicketManager, IoCContainer.TicketStatusManager,
				IoCContainer.FunctionManager, IoCContainer.User.Identity as IUser,
				Resources.WebSite.HomePageWeekDisplayFormat,
				Resources.WebSite.HomePageMonthDisplayFormat);


			_canViewTicket = _presenter.CanViewTicket();
			_canUpdateTicket = _presenter.CanEditTicket(); 
			_canUpdateProgressTicket = _presenter.CanUpdateProgressTicket(); ;
		}

		protected void _tickets_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Literal viewImage = e.Item.FindControl("_viewImage") as Literal;
				Literal editImage = e.Item.FindControl("_editImage") as Literal;
				Literal updateProgressImage = e.Item.FindControl("_updateProgressImage") as Literal;
				PlaceHolder viewLink = e.Item.FindControl("_viewLink") as PlaceHolder;
				PlaceHolder editLink = e.Item.FindControl("_editLink") as PlaceHolder;
				PlaceHolder updateProgressLink = e.Item.FindControl("_updateProgressLink") as PlaceHolder;

				viewImage.Text = Resources.WebSite.IconViewImageTag;
				editImage.Text = Resources.WebSite.IconEditImageTag;
				updateProgressImage.Text = Resources.WebSite.IconUpdateProgressImageTag;


				viewLink.Visible = _canViewTicket;
				editLink.Visible = _canUpdateTicket;
				updateProgressLink.Visible = _canUpdateProgressTicket;


			}

		}


		#region IHomeView Members

		public HomeViewDisplayFormat SelectedDisplayFormat
		{
			get
			{
				return (HomeViewDisplayFormat)Enum.Parse(typeof(HomeViewDisplayFormat), _displayDate.SelectedValue);
			}
			set
			{
				_displayDate.SelectedValue = value.ToString();
			}
		}

		public DateTime CurrentDate
		{
			get
			{
				return (DateTime)ViewState["CurrentDate"];
			}
			set
			{
				ViewState["CurrentDate"] = value;
			}
		}

		public string CurrentDateText
		{
			set { _currentDate.Text = value; }
		}

		public event EventHandler PreviewDateSelected
		{
			add { _prevDate.Click += value; }
			remove { _prevDate.Click -= value; }
		}

		public event EventHandler NextDateSelected
		{
			add { _nextDate.Click += value; }
			remove { _nextDate.Click -= value; }
		}

		public event EventHandler DisplayFormatChanged
		{
			add { _displayDate.SelectedIndexChanged += value; }
			remove { _displayDate.SelectedIndexChanged -= value; }
		}

		public TicketStatus[] Legends
		{
			set
			{
				_legends.DataSource = value;
				_legends.DataBind();
			}
		}

		#endregion

		#region IView Members


		public object DataSource
		{
			set { 
				_tickets.DataSource = value;
			}
		}

		public override void DataBind()
		{
			_tickets.DataBind();
			_noTicketsMessage.Visible = _tickets.Items.Count == 0;
			_tickets.Visible = !_noTicketsMessage.Visible;
		}



		#endregion

	}
}
