using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Common;
using System.Collections;

namespace Bagge.Seti.AlertsSender
{
	public class Storage: IStorage
	{
		#region IStorage Members

		private static Hashtable _data;

		private static Hashtable Data
		{
			get
			{
				if (_data == null)
					_data = new Hashtable();
				return _data;
			}
		}


		public void SetData(object key, object value)
		{
			if (Data.ContainsKey(key))
				Data[key] = value;
			else
				Data.Add(key, value);
		}

		public object GetData(object key)
		{
			if (Data.ContainsKey(key))
				return Data[key];
			return null;
		}

		#endregion
	}
}
