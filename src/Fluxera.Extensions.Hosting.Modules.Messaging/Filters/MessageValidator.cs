namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System.ComponentModel.DataAnnotations;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class MessageValidator : IMessageValidator
	{
		/// <inheritdoc />
		public void ValidateMessage<T>(T message)
		{
			Guard.Against.Null(message);

			Validator.ValidateObject(message, new ValidationContext(message), true);
		}
	}
}
