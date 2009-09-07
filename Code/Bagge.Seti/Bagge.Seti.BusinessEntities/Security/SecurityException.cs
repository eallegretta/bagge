using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace Bagge.Seti.BusinessEntities.Security
{
	[ActiveRecord]
	public class SecurityException: PrimaryKeyDomainObject<SecurityException, int>
	{
		
		[Property]
		public string ConstraintType
		{
			get;
			set;
		}

		public object Value
		{
			get
			{
				XmlSerializer ser = new XmlSerializer(GetPropertyType());
				using (var reader = new StringReader(SerializedValue))
				{
					return ser.Deserialize(reader);
				}
			}
			set
			{
				XmlSerializer ser = new XmlSerializer(value.GetType());
				using (var writer = new StringWriter())
				{
					ser.Serialize(writer, value);
					SerializedValue = writer.ToString();
				}
			}
		}

		private Type GetPropertyType()
		{
			return Type.GetType(SecureEntity.ClassFullQualifiedName).GetProperty(PropertyName).PropertyType;
		}

		[Property("Value")]
		private string SerializedValue
		{
			get;
			set;
		}

		[BelongsTo("RoleId")]
		public Role Role
		{
			get;
			set;
		}

		[BelongsTo("SecureEntityId")]
		public SecureEntity SecureEntity
		{
			get;
			set;
		}

		[Property]
		public string PropertyName
		{
			get;
			set;
		}

	}
}
