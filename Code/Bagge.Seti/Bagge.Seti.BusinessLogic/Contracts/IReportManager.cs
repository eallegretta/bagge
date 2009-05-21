using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IReportManager
	{
		/// <summary>
		/// Return a report based on the specified filters
		/// </summary>
		/// <typeparam name="T">T should be of type BaseReport</typeparam>
		/// <param name="filters">The filters to apply</param>
		/// <returns>Returns a list of BaseReport items</returns>
		IList<BaseReport> GetReport<T>(IList<FilterPropertyValue> filters);
	}
}
