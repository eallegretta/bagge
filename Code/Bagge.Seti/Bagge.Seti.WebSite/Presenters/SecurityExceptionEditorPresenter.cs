using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.BusinessEntities.Exceptions;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.WebSite.Model;
using Bagge.Seti.Extensions;
using Bagge.Seti.Security.Constraints;
using System.Reflection;

namespace Bagge.Seti.WebSite.Presenters
{
	public class SecurityExceptionEditorPresenter: EditorPresenter<SecurityExceptionViewModel, int>
	{
		ISecurityManager _manager;
		IRoleManager _roleManager;
		IFunctionManager _functionManager;

		protected new ISecurityExceptionEditorView View
		{
			get { return GetView<ISecurityExceptionEditorView>(); }
		}

		public SecurityExceptionEditorPresenter(ISecurityExceptionEditorView view, ISecurityManager manager, 
			IRoleManager roleManager, IFunctionManager functionManager)
			: base(view, null)
		{
			_manager = manager;
			_roleManager = roleManager;
			_functionManager = functionManager;

			View.Init += new EventHandler(View_Init);
		}

		void View_Init(object sender, EventArgs e)
		{
			View.DataBound += new EventHandler(View_DataBound);
		}

		void View_DataBound(object sender, EventArgs e)
		{
			switch (View.Mode)
			{
				case EditorAction.Insert:

					View.Roles = _roleManager.FindAllActive().Where(r => !r.IsSuperAdministratorRole).ToArray();
					View.SelectedRoleChanged += new EventHandler(View_SelectedRoleChanged);
					View.SelectedFunctionChanged += new EventHandler(View_SelectedFunctionChanged);
					View.SelectedEntityChanged += new EventHandler(View_SelectedEntityChanged);
					View.SelectedPropertyChanged += new EventHandler(View_SelectedPropertyChanged);
					if (!View.IsPostBack)
					{
						HideElements(HideElement.FromFunctions);
					}
					break;	
				case EditorAction.Update:
					if (!View.IsPostBack)
					{
						
						View.SelectedRoleId = SelectedEntity.Role.Id;
						View.SelectedFunctionId = SelectedEntity.Function.Id;
						View.SelectedEntityTypeName = SelectedEntity.ClassFullQualifiedName;
						View.SelectedPropertyName = SelectedEntity.PropertyName;
						var type = GetPropertyType();
						View.Constraints = Constraint.GetConstraintsForType(type);
						View.SelectedConstraintSymbol = SelectedEntity.ConstraintType;
						View.ValueType = GetValueType(type);
						View.Value = SelectedEntity.Value;
						View.ShowConstraintAndValue = true;
					}
					if (View.Mode == EditorAction.Update)
					{
						View.SelectedEntityChanged += new EventHandler(View_SelectedEntityChanged);
						View.SelectedPropertyChanged += new EventHandler(View_SelectedPropertyChanged);
					}
					break;
			}
		}

		private enum HideElement
		{
			FromFunctions = 0,
			FromEntities = 1,
			FromProperties = 2,
			FromConstraints = 3
		}

		private void HideElements(HideElement fromElement)
		{
			if (fromElement <= HideElement.FromFunctions)
			{
				View.ShowFunctions = false;
				View.Functions = null;
			}
			if (fromElement <= HideElement.FromEntities)
			{
				View.ShowEntities = false;
				View.Entities = null;
			}
			if (fromElement <= HideElement.FromProperties)
			{
				View.ShowProperties = false;
				View.Properties = null;
			}
			if (fromElement <= HideElement.FromConstraints)
			{
				View.ShowConstraintAndValue = false;
				View.Constraints = null;
			}
		}

		void View_SelectedPropertyChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(View.SelectedPropertyName))
			{
				View.ShowConstraintAndValue = true;

				var type = GetPropertyType();

				if (type == null)
					View.Constraints = null;
				else
					View.Constraints = Constraint.GetConstraintsForType(type);

				View.ValueType = GetValueType(type);
				View.Value = null;
			}
			else
				HideElements(HideElement.FromConstraints);
		}

		private Type GetPropertyType()
		{
			var type = (from prop in SecurizableAttribute.GetSecurizableProperties(Type.GetType(View.SelectedEntityTypeName))
						where prop.Property.Name == View.SelectedPropertyName
						select prop.Property.PropertyType).FirstOrDefault();
			return type;
		}

		private SecurityExceptionEditorValueType GetValueType(Type type)
		{
			if (type.IsOfType(true, typeof(DateTime), typeof(TimeSpan)))
				return SecurityExceptionEditorValueType.DateTime;
			else if (type.IsOfType(true,
				typeof(byte), typeof(sbyte), typeof(short), typeof(ushort),
				typeof(int), typeof(uint), typeof(long), typeof(ulong)))
				return SecurityExceptionEditorValueType.NumericInteger;
			else if(type.IsOfType(true, typeof(float)))
				return SecurityExceptionEditorValueType.NumericFloat;
			else if(type.IsOfType(true, typeof(double)))
				return SecurityExceptionEditorValueType.NumericDouble;
			else if(type.IsOfType(true, typeof(decimal)))
				return SecurityExceptionEditorValueType.NumericDecimal;
			else if (type.IsOfType(true, typeof(char)))
				return SecurityExceptionEditorValueType.Char;
			else if (type.IsOfType(true, typeof(bool)))
				return SecurityExceptionEditorValueType.Boolean;
			else
				return SecurityExceptionEditorValueType.String;
		}

		void View_SelectedEntityChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(View.SelectedEntityTypeName))
			{
				View.ShowProperties = true;
				View.Properties = SecurizableAttribute.GetSecurizableProperties(Type.GetType(View.SelectedEntityTypeName)).ToArray();
				HideElements(HideElement.FromConstraints);
			}
			else
				HideElements(HideElement.FromProperties);
		}

		


		void View_SelectedFunctionChanged(object sender, EventArgs e)
		{
			if (View.SelectedFunctionId.HasValue)
			{
				View.ShowEntities = true;
				View.Entities = _manager.FindAllSecureEntities(View.SelectedFunctionId.Value);
				HideElements(HideElement.FromProperties);
			}
			else
			{
				HideElements(HideElement.FromEntities);
			}
		}

		void View_SelectedRoleChanged(object sender, EventArgs e)
		{
			if (View.SelectedRoleId.HasValue)
			{
				View.ShowFunctions = true;
				View.Functions = _roleManager.Get(View.SelectedRoleId.Value).Functions.ToArray();
				HideElements(HideElement.FromEntities);
			}
			else
			{
				HideElements(HideElement.FromFunctions);
			}
		}


		public override object GetSelectedEntity()
		{
			return _manager.GetSecurityException(View.PrimaryKey);
		}

		public override void Select()
		{
			if (View.Mode == EditorAction.Update || View.Mode == EditorAction.View)
			{
				SelectedEntity = new SecurityExceptionViewModel(_manager.GetSecurityException(View.PrimaryKey));
				View.DataSource =SelectedEntity.ToSingleItemArray<SecurityExceptionViewModel>();
			}
		}

		public override void Save(SecurityExceptionViewModel entityViewModel)
		{
			Check.Require(View.SelectedRoleId.HasValue);
			Check.Require(View.SelectedFunctionId.HasValue);
			Check.Require(!string.IsNullOrEmpty(View.SelectedEntityTypeName));
			Check.Require(!string.IsNullOrEmpty(View.SelectedPropertyName));
			Check.Require(!string.IsNullOrEmpty(View.SelectedConstraintSymbol));
			Check.Require(View.Mode != EditorAction.View);

			var entity = new SecurityException();

			entity.Role = _roleManager.Get(View.SelectedRoleId.Value);
			entity.SecureEntity = _manager.GetSecureEntity(View.SelectedFunctionId.Value, View.SelectedEntityTypeName);
			entity.PropertyName = View.SelectedPropertyName;
			entity.ConstraintType = View.SelectedConstraintSymbol;
			entity.Value = View.Value;


			if (View.Mode == EditorAction.Update)
				entity.Id = View.PrimaryKey;

			_manager.Save(entity);
		}
	}
}
