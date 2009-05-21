﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface IReportView: IView
	{
		IList<FilterPropertyValue> Filters { get; }
	}
}