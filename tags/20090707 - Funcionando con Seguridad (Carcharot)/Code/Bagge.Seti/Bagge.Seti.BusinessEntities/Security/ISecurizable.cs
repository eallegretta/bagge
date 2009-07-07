using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.Constraints;

namespace Bagge.Seti.Security.BusinessEntities
{
	public interface ISecurizable
	{
		AccessibilityTypes GetAccesibility(string propertyName);
		void SetAccesibility(string propertyName, AccessibilityTypes accesibitily);
		void SetConstraint(string propertyName, Constraint constraint);
		IList<Constraint> GetConstraints(string propertyName);
	}
}
