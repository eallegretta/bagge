using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class Transaction: ITransaction
	{
		private TransactionScope _scope;

		public Transaction()
		{
			_scope = new TransactionScope();
		}

		#region ITransaction Members

		public void Commit()
		{
			_scope.VoteCommit();
		}

		public void Rollback()
		{
			_scope.VoteRollBack();
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			_scope.Dispose();
		}

		#endregion
	}
}
