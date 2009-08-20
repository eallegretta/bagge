using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.DataAccess
{
	public interface ITransaction: IDisposable
	{
		void Commit();
		void Rollback();
	}
}
