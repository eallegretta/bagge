using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Controls
{
	public class FilteredGridView: GridView
	{
		private static readonly object _eventFilter;

		public event EventHandler<FilteredGridViewEventArgs> Filter
		{
			add
			{
				base.Events.AddHandler(_eventFilter, value);
			}
			remove
			{
				base.Events.RemoveHandler(_eventFilter, value);
			}
		}

		protected virtual void OnFilter(FilteredGridViewEventArgs e)
		{
			EventHandler<FilteredGridViewEventArgs> handler = (EventHandler<FilteredGridViewEventArgs>)base.Events[_eventFilter];
			if (handler != null)
			{
				handler(this, e);
			}
		}

	}
}
