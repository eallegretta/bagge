using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;
using Bagge.Seti.BusinessEntities.Security;
using AjaxControlToolkit;
using Bagge.Seti.BusinessEntities;
using System.Linq.Expressions;
using Bagge.Seti.WebSite.Model;


namespace Bagge.Seti.WebSite
{
	public partial class SecurityExceptionsEditor : EditorPage<SecurityExceptionViewModel, int>, ISecurityExceptionEditorView
	{
		SecurityExceptionEditorPresenter _presenter;

		public SecurityExceptionsEditor()
		{
			_presenter = new SecurityExceptionEditorPresenter(this, IoCContainer.SecurityManager,
				IoCContainer.RoleManager, IoCContainer.FunctionManager);

		}

		#region ISecurityExceptionEditorView Members

		public int? SelectedRoleId
		{
			get { return GetControlValue("_role").ToInt32Nullable(); }
			set { SetControlValue("_role", value.HasValue ? value.Value.ToString() : null); }
		}

		public int? SelectedFunctionId
		{
			get { return GetControlValue("_function").ToInt32Nullable(); }
			set { SetControlValue("_function", value.HasValue ? value.Value.ToString() : null); }
		}

		public Bagge.Seti.BusinessEntities.Security.SecureEntity[] Entities
		{
			set
			{
				var entity = _details.FindControl("_entity") as DropDownList;
				if (entity != null)
				{
					entity.Items.Clear();
					entity.Items.Add(new ListItem());
					if (value != null)
					{
						foreach (var instance in value)
						{
							entity.Items.Add(new ListItem(SecurizableAttribute.GetName(instance.TargetType), instance.ClassFullQualifiedName));
						}
					}
					SetDropDownSelectedValue(entity);
				}
			}
		}

		public string SelectedEntityTypeName
		{
			get { return GetControlValue("_entity"); }
			set { SetControlValue("_entity", value); }
		}

		public Bagge.Seti.BusinessEntities.Security.PropertyInfoAndName[] Properties
		{
			set
			{
				var property = _details.FindControl("_property") as DropDownList;
				if (property != null)
				{
					property.Items.Clear();
					property.Items.Add(new ListItem());
					if (value != null)
					{
						foreach (var instance in value)
						{
							property.Items.Add(new ListItem(instance.Name, instance.Property.Name));
						}
					}
					SetDropDownSelectedValue(property);
				}
			}
		}

		public string SelectedPropertyName
		{
			get { return GetControlValue("_property"); }
			set { SetControlValue("_property", value); }
		}


		private void AddDropDownListEvent(string controlID, EventHandler handler)
		{
			var control = _details.FindControl(controlID) as DropDownList;
			if (control != null)
				control.SelectedIndexChanged += handler;
		}
		private void RemoveDropDownListEvent(string controlID, EventHandler handler)
		{
			var control = _details.FindControl(controlID) as DropDownList;
			if (control != null)
				control.SelectedIndexChanged += handler;
		}

		public string GetControlValue(string controlID)
		{
			var property = _details.FindControl(controlID);

			if (property is DropDownList)
			{
				string value = ((DropDownList)property).SelectedValue;
				if (value == string.Empty)
					value = Request.Form[property.UniqueID];
				return value;
			}
			else
				return ((HiddenField)property).Value;

			//return Request.Form[property.UniqueID];
		}

		public void SetControlValue(string controlID, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				var property = Details.FindControl(controlID);
				if (property is DropDownList)
				{
					((DropDownList)property).ClearSelection();
					var item = ((DropDownList)property).Items.FindByValue(value);
					if (item != null)
						item.Selected = true;
				}
				else
					((HiddenField)property).Value = value;
			}
		}

		public event EventHandler SelectedPropertyChanged
		{
			add
			{
				AddDropDownListEvent("_property", value);
			}
			remove
			{
				RemoveDropDownListEvent("_property", value);
			}
		}

		public event EventHandler SelectedEntityChanged
		{
			add
			{
				AddDropDownListEvent("_entity", value);
			}
			remove
			{
				RemoveDropDownListEvent("_entity", value);
			}
		}


		public Bagge.Seti.Security.Constraints.Constraint[] Constraints
		{
			set
			{
				var constraint = _details.FindControl("_constraint") as DropDownList;
				if (constraint != null)
				{
					constraint.Items.Clear();
					constraint.Items.Add(new ListItem());
					if (value != null)
					{
						foreach (var instance in value)
						{
							constraint.Items.Add(new ListItem(instance.ToString(), instance.Symbol));
						}
					}
					SetDropDownSelectedValue(constraint);
				}
			}
		}

		public string SelectedConstraintSymbol
		{
			get { return GetControlValue("_constraint"); }
			set { SetControlValue("_constraint", value); }
		}

		private RETTYPE GetConstraintValue<RETTYPE>(string ctrlID) where RETTYPE : IConvertible
		{
			var ctrl = _details.FindControl(ctrlID);
			string value = Request.Form[ctrl.UniqueID];
			if (string.IsNullOrEmpty(value))
				return default(RETTYPE);

			if (ctrl is CheckBox)
				value = value.Replace("on", "True");

			return (RETTYPE)Convert.ChangeType(value, typeof(RETTYPE));
		}

		private CTRLTYPE GetControl<CTRLTYPE>(string controlID) where CTRLTYPE : Control
		{
			return _details.FindControl(controlID) as CTRLTYPE;
		}

		public object Value
		{
			get
			{
				switch (ValueType)
				{
					case SecurityExceptionEditorValueType.NumericInteger:
						return GetConstraintValue<string>("_valueNumber").ToInt32Nullable();
					case SecurityExceptionEditorValueType.NumericDecimal:
						return GetConstraintValue<string>("_valueNumber").ToFloatNullable();
					case SecurityExceptionEditorValueType.Char:
						return GetConstraintValue<string>("_valueChar");
					case SecurityExceptionEditorValueType.DateTime:
						return GetControl<Bagge.Seti.WebSite.Controls.Calendar>("_valueDate").Date;
					case SecurityExceptionEditorValueType.Boolean:
						return GetConstraintValue<bool>("_valueBoolean");
					default:
						return GetConstraintValue<string>("_valueString");
				}
			}
			set
			{
				switch (ValueType)
				{
					case SecurityExceptionEditorValueType.NumericInteger:
					case SecurityExceptionEditorValueType.NumericDecimal:
						GetControl<TextBox>("_valueNumber").Text = value as string;
						break;
					case SecurityExceptionEditorValueType.Char:
						GetControl<TextBox>("_valueChar").Text = value as string;
						break;
					case SecurityExceptionEditorValueType.DateTime:
						GetControl<Bagge.Seti.WebSite.Controls.Calendar>("_valueDate").Date = value as DateTime?;
						break;
					case SecurityExceptionEditorValueType.Boolean:
						if (value is bool)
							GetControl<CheckBox>("_valueBoolean").Checked = (bool)value;
						else
							GetControl<CheckBox>("_valueBoolean").Checked = false;
						break;
					default:
						GetControl<TextBox>("_valueString").Text = value as string;
						break;
				}
			}
		}


		private View GetView(string viewId)
		{
			return _details.FindControl(viewId) as View;
		}

		public SecurityExceptionEditorValueType ValueType
		{
			set
			{
				ViewState["ValueType"] = value;
				var valueView = _details.FindControl("_valueView") as MultiView;
				switch (value)
				{
					case SecurityExceptionEditorValueType.NumericInteger:
						valueView.SetActiveView(GetView("_valueViewNumber"));
						((MaskedEditExtender)_details.FindControl("_valueNumberExt")).Mask = "9999999999";
						break;
					case SecurityExceptionEditorValueType.NumericDecimal:
						valueView.SetActiveView(GetView("_valueViewNumber") as View);
						((MaskedEditExtender)_details.FindControl("_valueNumberExt")).Mask = "9999999999.99";
						break;
					case SecurityExceptionEditorValueType.DateTime:
						valueView.SetActiveView(GetView("_valueViewCalendar"));
						break;
					case SecurityExceptionEditorValueType.Char:
						valueView.SetActiveView(GetView("_valueViewChar"));
						break;
					case SecurityExceptionEditorValueType.Boolean:
						valueView.SetActiveView(GetView("_valueViewBoolean"));
						break;
					default:
						valueView.SetActiveView(GetView("_valueViewString"));
						break;
				}
			}
			private get
			{
				return (SecurityExceptionEditorValueType)ViewState["ValueType"];
			}
		}

		protected override EditorPresenter<SecurityExceptionViewModel, int> Presenter
		{
			get { return _presenter; }
		}

		protected override CompositeDataBoundControl Details
		{
			get { return _details; }
		}

		protected override Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}

		protected override bool ShowRequiredInformationLabel
		{
			get { return true; }
		}

		public void SetDropDownSelectedValue(DropDownList ddl)
		{
			string value = Request.Form[ddl.UniqueID];
			if (!string.IsNullOrEmpty(value))
			{
				var item = ddl.Items.FindByValue(value);
				if (item != null)
					item.Selected = true;
			}
		}

		public Bagge.Seti.Security.BusinessEntities.Role[] Roles
		{
			set
			{
				var roles = _details.FindControl("_role") as DropDownList;
				if (roles != null)
				{
					roles.DataSource = value;
					roles.DataBind();
					//SetDropDownSelectedValue(roles);
				}
			}
		}

		public Bagge.Seti.Security.BusinessEntities.Function[] Functions
		{
			set
			{
				var functions = _details.FindControl("_function") as DropDownList;
				if (functions != null)
				{
					functions.DataSource = value;
					functions.DataBind();
					SetDropDownSelectedValue(functions);
				}
			}
		}

		public event EventHandler SelectedRoleChanged
		{
			add
			{
				AddDropDownListEvent("_role", value);
			}
			remove
			{
				RemoveDropDownListEvent("_role", value);
			}
		}

		public event EventHandler SelectedFunctionChanged
		{
			add
			{
				AddDropDownListEvent("_function", value);
			}
			remove
			{
				RemoveDropDownListEvent("_function", value);
			}
		}
		public bool ShowFunctions
		{
			set
			{
				if (value)
					ShowHideControls("_selectRoleMessage", "_functionPanel");
				else
					ShowHideControls("_functionPanel", "_selectRoleMessage");
			}
		}

		public bool ShowEntities
		{
			set
			{
				if (value)
					ShowHideControls("_selectFunctionMessage", "_entityPanel");
				else
					ShowHideControls("_entityPanel", "_selectFunctionMessage");
			}
		}

		public bool ShowProperties
		{
			set
			{
				if (value)
					ShowHideControls("_selectEntityMessage", "_propertyPanel");
				else
					ShowHideControls("_propertyPanel", "_selectEntityMessage");
			}
		}

		public bool ShowConstraintAndValue
		{
			set
			{
				if (value)
				{
					ShowHideControls("_selectPropertyMessage", "_constraintPanel");
					ShowHideControls("_selectPropertyMessage2", "_valuePanel");
				}
				else
				{
					ShowHideControls("_constraintPanel", "_selectPropertyMessage");
					ShowHideControls("_valuePanel", "_selectPropertyMessage2");
				}
			}
		}

		private void ShowHideControls(string controlIDToHide, string controlIDToShow)
		{
			_details.FindControl(controlIDToHide).Visible = false;
			_details.FindControl(controlIDToShow).Visible = true;
		}

		#endregion
	}
}
