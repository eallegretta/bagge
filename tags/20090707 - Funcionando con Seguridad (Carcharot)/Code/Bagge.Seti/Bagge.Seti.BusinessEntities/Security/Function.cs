﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;
using System.Reflection;
using System.Diagnostics;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.Security.BusinessEntities
{
	[Flags]
	public enum FunctionAction
	{
		Create,
		Retrieve,
		Update,
		Delete,
		NotSet
	}

	[ActiveRecord("[Function]")]
	[Serializable]
	public class Function : PrimaryKeyWithNameDomainObject<Function, int>
	{
		[Property]
		public string FullQualifiedName
		{
			get;
			set;
		}

		public static char ActionToChar(FunctionAction action)
		{
			switch (action)
			{
				case FunctionAction.Create:
					return 'C';
				case FunctionAction.Retrieve:
					return 'R';
				case FunctionAction.Update:
					return 'U';
				default:
					return 'D';
			}
		}

		public static FunctionAction CharToAction(char action)
		{
			switch (action)
			{
				case 'C':
					return FunctionAction.Create;
				case 'R':
					return FunctionAction.Retrieve;
				case 'U':
					return FunctionAction.Update;
				default:
					return FunctionAction.Delete;
			}
		}

		[Property]
		public char Action
		{
			get; 
			set;
		}

		
		[HasAndBelongsToMany(Table="RoleFunction", ColumnKey="FunctionId", ColumnRef="RoleId")]
		public virtual IList<Role> Roles
		{
			get; set;
		}

		[HasMany(typeof(SecurityException))]
		public virtual IList<SecurityException> SecurityExceptions
		{
			get; set;
		}
	}
}
