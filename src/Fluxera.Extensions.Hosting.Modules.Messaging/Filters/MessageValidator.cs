namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	[UsedImplicitly]
	internal sealed class MessageValidator : IMessageValidator
	{
		private readonly MessagingOptions options;

		public MessageValidator(IOptions<MessagingOptions> options)
		{
			this.options = options.Value;
		}

		/// <inheritdoc />
		public void ValidateMessage<T>(T message)
		{
			Guard.Against.Null(message, nameof(message));

			//if(this.options.ValidationEnabled)
			//{
			//	Validator.ValidateObject(message, new ValidationContext(message), true);
			//}
		}
	}
}
