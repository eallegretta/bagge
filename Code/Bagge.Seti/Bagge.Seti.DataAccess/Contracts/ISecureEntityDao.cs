using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface ISecureEntityDao
	{
		SecureEntity[] FindAll(int functionId);
		SecureEntity Get(int functionId, string classFullQualifiedName);
	}
}
