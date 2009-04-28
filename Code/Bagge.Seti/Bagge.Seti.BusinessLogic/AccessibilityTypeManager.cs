using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.BusinessLogic.Properties;

namespace Bagge.Seti.BusinessLogic
{
	public class AccessibilityTypeManager: IAccessibilityTypeManager 
	{
		#region IAccessibilityTypeManager Members

		protected virtual IAccessibilityDao Dao
		{
			get;
			set;
		}

		public AccessibilityTypeManager(IAccessibilityDao dao)
		{
			Dao = dao;
		}


		public AccessibilityTypes GetUserAccessibilityForProperty(IUser user, Type targetObject, string propertyName)
		{
			if(user.IsSuperAdministrator)
				return AccessibilityTypes.Edit;

			var filteredFunctions = (from function in user.Functions
									 where function.MemberType == 'P' &&
											function.MemberName == propertyName &&
											function.TargetType.Equals(targetObject)
									 select function).ToArray();

			//If no functions, return default behavior
			if (filteredFunctions.Length == 0)
				return Settings.Default.DefaultPropertyAccessibilityType;


			bool canEdit = (from function in filteredFunctions
							where function.Accessibility.Equals(AccessibilityTypes.Edit)
							select function).Count() > 0;
			if (canEdit)
				return AccessibilityTypes.Edit;

			bool canView = (from function in filteredFunctions
							where function.Accessibility.Equals(AccessibilityTypes.View)
							select function).Count() > 0;

			if (canView)
				return AccessibilityTypes.View;

			return AccessibilityTypes.None;
		}

		public AccessibilityTypes GetUserAccessibilityForMethod(IUser user, Type targetObject, string methodName)
		{
			if (user.IsSuperAdministrator)
				return AccessibilityTypes.Execute;

			var filteredFunctions = (from function in user.Functions
									 where function.MemberType == 'M' &&
											function.MemberName == methodName &&
											function.TargetType.Equals(targetObject)
									 select function).ToArray();

			//If no functions, return default behavior
			if (filteredFunctions.Length == 0)
				return Settings.Default.DefaultMethodAccessibilityType;

			bool canExecute = (from function in filteredFunctions
							   where function.Accessibility.Equals(AccessibilityTypes.Execute)
							   select function).Count() > 0;
			if (canExecute)
				return AccessibilityTypes.Execute;

			return AccessibilityTypes.None;
		}

		#endregion

		public AccessibilityType[] FindAllByType(AccessibilityTypeType type)
		{
			return Dao.FindAllByType(type);
		}

		#region IFindManager<AccessibilityType,byte> Members

		public AccessibilityType[] FindAll()
		{
			return FindAll(null, null);
		}

		public AccessibilityType[] FindAllOrdered(string orderBy)
		{
			return FindAll(orderBy, true);
		}

		public AccessibilityType[] FindAllOrdered(string orderBy, bool ascending)
		{
			return FindAll(orderBy, ascending);
		}

		protected virtual AccessibilityType[] FindAll(string orderBy, bool? ascending)
		{
			return Dao.FindAll(orderBy, ascending);
		}

		public AccessibilityType[] FindAllByProperty(string property, object value)
		{
			return FindAllByProperty(property, value, null, null);
		}

		public AccessibilityType[] FindAllByPropertyOrdered(string property, object value, string orderBy)
		{
			return FindAllByProperty(property, value, orderBy, true);
		}

		public AccessibilityType[] FindAllByPropertyOrdered(string property, object value, string orderBy, bool ascending)
		{
			return FindAllByProperty(property, value, orderBy, ascending);
		}

		protected virtual AccessibilityType[] FindAllByProperty(string property, object value, string orderBy, bool? ascending)
		{
			return Dao.FindAllByProperty(property, value, orderBy, ascending);
		}

		public AccessibilityType[] FindAllByProperties(IList<FilterPropertyValue> filter)
		{
			return FindAllByProperties(filter, null, null);
		}

		public AccessibilityType[] FindAllByPropertiesOrdered(IList<FilterPropertyValue> filter, string orderBy)
		{
			return FindAllByProperties(filter, orderBy, true);
		}

		public AccessibilityType[] FindAllByPropertiesOrdered(IList<FilterPropertyValue> filter, string orderBy, bool ascending)
		{
			return FindAllByProperties(filter, orderBy, ascending);
		}

		protected virtual AccessibilityType[] FindAllByProperties(IList<FilterPropertyValue> filter, string orderBy, bool? ascending)
		{
			return Dao.FindAllByProperties(filter, orderBy, ascending);
		}

		#endregion

		#region IGetManager<AccessibilityType,byte> Members

		public Bagge.Seti.Security.BusinessEntities.AccessibilityType Get(byte id)
		{
			return Dao.Get(id);
		}

		#endregion
	}
}
