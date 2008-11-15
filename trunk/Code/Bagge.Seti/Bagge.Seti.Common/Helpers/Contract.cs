using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Common.Properties;
using System.Globalization;
using System.ComponentModel;
using System.Collections;

namespace Bagge.Seti.Common.Helpers
{
	/// <summary>
	/// An utility class to check for exceptions.
	/// </summary>
	public static class Contract
	{
		/// <summary>
		/// Checks that a property is not <see langword="null"/>.
		/// </summary>
		/// <param name="property">The property <see cref="PropertyDescriptor"/>.</param>
		/// <param name="propertyName">The property name.</param>
		/// <exception cref="InvalidOperationException">The property is null.</exception>
		public static void PropertyNotNull(PropertyDescriptor property, string propertyName)
		{
			if (property == null)
				throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PropertyNotFoundInType, propertyName));
		}

		/// <summary>
		/// Checks that the value of an argument is not <see langword="null"/>.
		/// </summary>
		/// <param name="argumentValue">The value of the argument to check.</param>
		/// <param name="argumentName">The name of the argument.</param>
		/// <exception cref="ArgumentNullException">The argument value is <see langword="null"/>.</exception>
		public static void ArgumentNotNull(object argumentValue, string argumentName)
		{
			if (argumentValue == null)
				throw new ArgumentNullException(argumentName);
		}

		/// <summary>
		/// Checks a collection to be not empty nor <see langword="null"/>.
		/// </summary>
		/// <param name="collection">The <see cref="ICollection"/> object to check.</param>
		/// <param name="message">The exception message when the collection is empty.</param>
		/// <param name="argumentName">The name of the argument referencing the collection.</param>
		/// <exception cref="ArgumentNullException">The collection is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">The collection is empty.</exception>
		public static void CollectionNotNullNorEmpty(ICollection collection, string message, string argumentName)
		{
			if (collection == null)
				throw new ArgumentNullException(argumentName);

			if (collection.Count == 0)
				throw new ArgumentException(message, argumentName);
		}

		/// <summary>
		/// Check an <see langword="int"/> value to be positive.
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <param name="argumentName">The name of the argument containing the value.</param>
		/// <exception cref="ArgumentException">The value is negative.</exception>
		public static void ArgumentNonNegative(int value, string argumentName)
		{
			if (value < 0)
				throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, Resources.CannotBeNegative, argumentName));
		}
	}
}
