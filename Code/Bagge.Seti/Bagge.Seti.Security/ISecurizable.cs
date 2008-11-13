using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.Security
{
	public interface ISecurizable
	{
		bool IsAccesible(string propertyName);
		void SetAccesibility(string propertyName, bool isAccesible);
		void SetConstraint(string propertyName, Constraint constraint);
		IList<Constraint> GetConstraints(string propertyName);
	}
}
