using System;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface IDao<T, PK>: 
		IGetDao<T, PK>, 
		IFindDao<T, PK>, 
		ISlicedFindDao<T, PK>, 
		ICreateDao<T, PK>, 
		IUpdateDao<T, PK>, 
		IDeleteDao<T, PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		
	}
}
