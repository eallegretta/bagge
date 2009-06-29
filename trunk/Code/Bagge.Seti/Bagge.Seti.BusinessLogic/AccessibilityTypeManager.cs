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
	[Securizable("Securizable_AccessibilityTypeManager", typeof(AccessibilityTypeManager))]
	public partial class AccessibilityTypeManager : IAccessibilityTypeManager 
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


			var filteredExceptions = (from exception in user.CurrentFunction.SecurityExceptions
									 where exception.MemberType == 'P' &&
											exception.MemberName == propertyName &&
											exception.TargetType.Equals(targetObject)
									 select exception).ToArray();

			//If no functions, return default behavior
			if (filteredExceptions.Length == 0)
				return Settings.Default.DefaultPropertyAccessibilityType;


			bool canEdit = (from exception in filteredExceptions
							where exception.Accessibility.Equals(AccessibilityTypes.Edit)
							select exception).Count() > 0;
			if (canEdit)
				return AccessibilityTypes.Edit;

			bool canView = (from exception in filteredExceptions
							where exception.Accessibility.Equals(AccessibilityTypes.View)
							select exception).Count() > 0;

			if (canView)
				return AccessibilityTypes.View;

			return AccessibilityTypes.None;
		}

		public AccessibilityTypes GetUserAccessibilityForMethod(IUser user, Type targetObject, string methodName)
		{
			if (user.IsSuperAdministrator)
				return AccessibilityTypes.Execute;

			var filteredExceptions = (from exception in user.CurrentFunction.SecurityExceptions
									 where exception.MemberType == 'M' &&
											exception.MemberName == methodName &&
											exception.TargetType.Equals(targetObject)
									 select exception).ToArray();

			//If no functions, return default behavior
			if (filteredExceptions.Length == 0)
				return Settings.Default.DefaultMethodAccessibilityType;

			bool canExecute = (from exception in filteredExceptions
							   where exception.Accessibility.Equals(AccessibilityTypes.Execute)
							   select exception).Count() > 0;
			if (canExecute)
				return AccessibilityTypes.Execute;

			return AccessibilityTypes.None;
		}

		#endregion

		[Securizable("Securizable_AccessibilityTypeManager_FindAllByType", typeof(AccessibilityTypeManager))]
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

		[Securizable("Securizable_AccessibilityTypeManager_FindAll", typeof(AccessibilityTypeManager))]
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

		[Securizable("Securizable_AccessibilityTypeManager_FindAllByProperty", typeof(AccessibilityTypeManager))]
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

		[Securizable("Securizable_AccessibilityTypeManager_FindAllByProperties", typeof(AccessibilityTypeManager))]
		protected virtual AccessibilityType[] FindAllByProperties(IList<FilterPropertyValue> filter, string orderBy, bool? ascending)
		{
			return Dao.FindAllByProperties(filter, orderBy, ascending);
		}

		#endregion

		#region IGetManager<AccessibilityType,byte> Members

		[Securizable("Securizable_AccessibilityTypeManager_Get", typeof(AccessibilityTypeManager))]
		public Bagge.Seti.Security.BusinessEntities.AccessibilityType Get(byte id)
		{
			return Dao.Get(id);
		}

		#endregion
	}
}
