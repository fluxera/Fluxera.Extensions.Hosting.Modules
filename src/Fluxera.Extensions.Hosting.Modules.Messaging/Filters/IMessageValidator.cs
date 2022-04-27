namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using Fluxera.Extensions.Validation;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a message validator.
	/// </summary>
	[PublicAPI]
	public interface IMessageValidator
	{
		/// <summary>
		///     Validates the given message. Will thrown an exception of validation fails.
		///     NOTE: Validation must be enabled in the message bus settings.
		/// </summary>
		/// <typeparam name="T">The message type.</typeparam>
		/// <param name="message">The message to validate.</param>
		/// <exception cref="ValidationException">Thrown when validation fails.</exception>
		void ValidateMessage<T>(T message);
	}
}
