using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.WebSite.Model;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.Security.Constraints;

namespace Bagge.Seti.WebSite.Views
{
	public enum SecurityExceptionEditorValueType
	{
		NumericInteger,
		NumericFloat,
		NumericDouble,
		NumericDecimal,
		String,
		Char,
		DateTime,
		Boolean,
	}

	public interface ISecurityExceptionEditorView : IEditorView<int>
	{
		int? SelectedRoleId { get; set; }
		Role[] Roles { set; }


		int? SelectedFunctionId { get; set; }
		Function[] Functions { set; }


		SecureEntity[] Entities { set; }

		string SelectedEntityTypeName { get; set; }

		PropertyInfoAndName[] Properties { set; }

		string SelectedPropertyName { get; set; }

		event EventHandler SelectedRoleChanged;

		event EventHandler SelectedFunctionChanged;

		event EventHandler SelectedEntityChanged;

		event EventHandler SelectedPropertyChanged;

		bool ShowFunctions { set; }

		bool ShowEntities { set; }

		bool ShowProperties { set; }

		bool ShowConstraintAndValue { set; }

		Constraint[] Constraints { set; }

		string SelectedConstraintSymbol { get; set; }

		object Value { get; set; }

		SecurityExceptionEditorValueType ValueType { set; }

		


	}
}
