namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using Fluxera.Extensions.Validation;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class MessageValidator : IMessageValidator
	{
		private readonly IValidationService validationService;

		public MessageValidator(IValidationService validationService)
		{
			this.validationService = validationService;
		}

		/// <inheritdoc />
		public void ValidateMessage<T>(T message)
		{
			Guard.Against.Null(message);

			this.validationService.ValidateAndThrow(message);
		}
	}
}
