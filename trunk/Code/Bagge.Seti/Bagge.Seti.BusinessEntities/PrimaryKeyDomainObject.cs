using System;
using Castle.ActiveRecord;
using Bagge.Seti.Security;
using System.Collections.Generic;

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

		private Dictionary<string, bool> _accesibilities;
		private Dictionary<string, IList<Constraint>> _constraints;

		private Dictionary<string, bool> Accesibilities
		{
			get
			{
				if (_accesibilities == null)
					_accesibilities = new Dictionary<string, bool>();
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

		public bool IsAccesible(string propertyName)
		{
			if (Accesibilities.ContainsKey(propertyName))
				return Accesibilities[propertyName];
			return true;
		}

		public void SetAccesibility(string propertyName, bool isAccesible)
		{
			if (!Accesibilities.ContainsKey(propertyName))
				Accesibilities.Add(propertyName, isAccesible);
			else
				Accesibilities[propertyName] = isAccesible;
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
