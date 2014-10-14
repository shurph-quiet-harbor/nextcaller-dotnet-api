namespace NextCallerApi.Entities
{
	/// <summary>
	/// Represents validation result: success flag, message.
	/// </summary>
	public class ValidationResult
	{
		/// <summary>
		/// String, describing validation result. Is null, if validation is successful.
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		/// Flag, indicating validation result.
		/// </summary>
		public bool IsValid { get; private set; }

		/// <summary>
		/// Initializes VaidationResult instance.
		/// </summary>
		/// <param name="isValid">Flag, indicating validation result.</param>
		/// <param name="message">String, describing validation result. Is null, if validation is successful.</param>
		public ValidationResult(bool isValid, string message)
		{
			Message = message;
			IsValid = isValid;
		}
	}
}
