using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Globalization;
using Bagge.Seti.Common.Properties;

namespace Bagge.Seti.Common.Helpers
{
	public static class TypeDescriptionHelper
	{
		/// <summary>
		/// Searches for a valid property.
		/// </summary>
		/// <param name="properties">The <see cref="PropertyDescriptorCollection"/> to search.</param>
		/// <param name="propertyName">The name of the property to look for.</param>
		/// <returns>A valid <see cref="PropertyDescriptor"/> for the requested property name.</returns>
		/// <exception cref="ArgumentNullException">The <paramref name="propertyName"/> is null.</exception>
		/// <exception cref="InvalidOperationException">The property is not present in the collection.</exception>
		public static PropertyDescriptor GetValidProperty(PropertyDescriptorCollection properties, string propertyName)
		{
			Contract.ArgumentNotNull(properties, "properties");

			PropertyDescriptor property = properties.Find(propertyName, false);
			Contract.PropertyNotNull(property, propertyName);
			return property;
		}

		/// <summary>
		/// Sets the properties values on the <paramref name="existing"/> object from the specified values <see cref="IDictionary"/>.
		/// </summary>
		/// <param name="values">An <see cref="IDictionary"/> object with the name/value pairs for the properties.</param>
		/// <param name="existing">The object to set the properties to.</param>
		/// <exception cref="InvalidOperationException">Cannot set the value of a read-only property.</exception>
		/// <exception cref="ArgumentNullException">Either the <paramref name="values"/> or <paramref name="existing"/> arguments are null.</exception>
		public static void BuildInstance(IDictionary values, object existing)
		{
			Contract.ArgumentNotNull(values, "values");
			Contract.ArgumentNotNull(existing, "existing");

			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(existing);
			foreach (DictionaryEntry entry in values)
			{
				string propertyName = entry.Key.ToString();
				PropertyDescriptor property = GetValidProperty(properties, propertyName);
				if (property.IsReadOnly)
					throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PropertyIsReadOnly, propertyName));

				object value = GetObjectValue(entry.Value, property.PropertyType, propertyName);
				property.SetValue(existing, value);
			}
		}

		/// <summary>
		/// Checks that the type <paramref name="item"/> object is the same as the type specified by <paramref name="expectedType"/>.
		/// </summary>
		/// <param name="item">An object to check its type.</param>
		/// <param name="expectedType">The expected type for the object.</param>
		/// <exception cref="InvalidOperationException">The type of the item object is not the expected type.</exception>
		/// <exception cref="ArgumentNullException">Either the <paramref name="item"/> or the <paramref name="expectedType"/> arguments ares null.</exception>
		public static void ThrowIfInvalidType(object item, Type expectedType)
		{
			Contract.ArgumentNotNull(item, "item");
			Contract.ArgumentNotNull(expectedType, "expectedType");

			if (!expectedType.IsAssignableFrom(item.GetType()))
				throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.InvalidObjectType, expectedType.FullName, item.GetType().FullName));
		}

		/// <summary>
		/// Checks that the specified type has a default constructor.
		/// </summary>
		/// <param name="type">The type to check for.</param>
		/// <exception cref="InvalidOperationException">The specified type has not a default constructor.</exception>
		public static void ThrowIfNoDefaultConstructor(Type type)
		{
			Contract.ArgumentNotNull(type, "type");

			if (type.GetConstructor(Type.EmptyTypes) == null)
				throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.DefaultConstructorMissing, type.FullName));
		}

		/// <summary>
		/// Converts an object value to the specified type.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to convert the value to.</param>
		/// <param name="paramName">The name of the parameter holding the value.</param>
		/// <returns>The converted value.</returns>
		public static object GetObjectValue(object value, Type targetType, string paramName)
		{
			Contract.ArgumentNotNull(targetType, "targetType");

			if ((value == null) || targetType.IsInstanceOfType(value))
				return value;

			value = TransformType(value, targetType, paramName);
			return value;
		}

		private static object TransformType(object value, Type targetType, string paramName)
		{
			Contract.ArgumentNotNull(targetType, "targetType");

			string stringValue = value as string;
			if (stringValue == null)
				return value;

			TypeConverter converter = TypeDescriptor.GetConverter(targetType);
			try
			{
				value = converter.ConvertFromString(stringValue);
			}
			catch
			{
				throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.CannotConvertType, paramName, typeof(string).FullName, targetType.FullName));
			}
			return value;
		}
	}
}
