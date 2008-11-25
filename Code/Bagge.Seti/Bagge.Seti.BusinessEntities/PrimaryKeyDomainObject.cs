using System;
using Castle.ActiveRecord;
using Bagge.Seti.Security;
using System.Collections.Generic;
using Bagge.Seti.Security.Constraints;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessEntities
{
	[Serializable]
	public class PrimaryKeyDomainObject<T, PK>: IEquatable<PrimaryKeyDomainObject<T, PK>>, ISecurizable
	{
		public PrimaryKeyDomainObject():this(default(PK))
		{
		}
		public PrimaryKeyDomainObject(PK id)
		{
			Id = id;
		}

		
		[PrimaryKey]
		public virtual PK Id { get; set; }


		#region IEquatable<PrimaryKeyDomainObject> Members

		public bool Equals(PrimaryKeyDomainObject<T, PK> other)
		{
			return Id.Equals(other.Id);
		}

		#endregion

		#region ISecurizable Members

		private Dictionary<string, AccesibilityType> _accesibilities;
		private Dictionary<string, IList<Constraint>> _constraints;

		private Dictionary<string, AccesibilityType> Accesibilities
		{
			get
			{
				if (_accesibilities == null)
					_accesibilities = new Dictionary<string, AccesibilityType>();
				return _accesibilities;
			}
		}

		private Dictionary<string, IList<Constraint>> Constraints
		{
			get
			{
				if (_constraints == null)
					_constraints = new Dictionary<string, IList<Constraint>>();
				return _constraints;
			}
		}

		public AccesibilityType GetAccesibility(string propertyName)
		{
			if (Accesibilities.ContainsKey(propertyName))
				return Accesibilities[propertyName];
			return AccesibilityType.Edit;
		}

		public void SetAccesibility(string propertyName, AccesibilityType accesibility)
		{
			if (!Accesibilities.ContainsKey(propertyName))
				Accesibilities.Add(propertyName, accesibility);
			else
				Accesibilities[propertyName] = accesibility;
		}

		public void SetConstraint(string propertyName, Constraint constraint)
		{
			if (!Constraints.ContainsKey(propertyName))
			{
				List<Constraint> constraints = new List<Constraint>();
				constraints.Add(constraint);
				Constraints.Add(propertyName, constraints);
			}
			else
				Constraints[propertyName].Add(constraint);
		}

		public IList<Constraint> GetConstraints(string propertyName)
		{
			if (Constraints.ContainsKey(propertyName))
				return Constraints[propertyName];
			return null;
		}

		#endregion
	}
}
