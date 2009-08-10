using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.Common
{
	public interface IStorage
	{
		void SetData(object key, object value);
		object GetData(object key);
	}
}
