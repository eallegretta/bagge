using System;
using System.Collections.Generic;
using System.Text;
using Bagge.Seti.BusinessEntities;


namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IManager<T, PK>: 
		IFindManager<T, PK>,
		ISlicedFindManager<T, PK>,
		IGetManager<T, PK>,
		ICreateManager<T, PK>,
		IUpdateManager<T, PK>,
		IDeleteManager<T, PK>

		where T: PrimaryKeyDomainObject<T,PK>
	{
	
		
		
		
		
		
	}
}
