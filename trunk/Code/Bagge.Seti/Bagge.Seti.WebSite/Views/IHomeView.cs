using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public enum HomeViewDisplayFormat
	{
		Weekly,
		Monthly,
	}

	public interface IHomeView: IView
	{
		HomeViewDisplayFormat SelectedDisplayFormat { get; set; }
		DateTime CurrentDate { get; set; }
		string CurrentDateText { set; }
		TicketStatus[] Legends { set; } 
		event EventHandler DisplayFormatChanged;
		event EventHandler RewindDateSelected;
		event EventHandler PreviewDateSelected;
		event EventHandler NextDateSelected;
		event EventHandler FastForwardDateSelected;
	}
}
