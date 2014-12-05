using System;
using System.ComponentModel;
using System.Reflection;


namespace NextCallerApi
{
	internal static class Utility
	{
		private const string ArgumentExceptionMessageTemplate = "Parameter name: {0}.";

		/// <summary>
		/// Throws ArgumentException, if provided condition is failed.
		/// </summary>
		/// <param name="isParameterValid">Condition to check.</param>
		/// <param name="parameterName">Name of a validated parameter.</param>
		/// <param name="message">Optional ArgumentException message.</param>
		public static void EnsureParameterValid(bool isParameterValid, string parameterName, string message = null)
		{
			if (!isParameterValid)
			{
				throw string.IsNullOrEmpty(message)
					? new ArgumentException(string.Format(ArgumentExceptionMessageTemplate, parameterName), parameterName)
					: new ArgumentException(message, parameterName);
			}
		}

		/// <summary>
		/// Tries to get DescriptionAttribute value of given enum. If the attribute is missing, returns enum's string representation.
		/// </summary>
		/// <param name="enumeration">Enum to get description of.</param>
		/// <returns></returns>
		public static string GetDescription(this Enum enumeration)
		{
			FieldInfo fi = enumeration.GetType().GetField(enumeration.ToString());

			DescriptionAttribute[] attributes =
				(DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			return attributes.Length > 0 ? attributes[0].Description : enumeration.ToString();
		}

	}
}
