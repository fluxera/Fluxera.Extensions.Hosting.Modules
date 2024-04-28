namespace Fluxera.Extensions.Hosting.Modules.Application.Validation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using global::FluentValidation;
	using global::FluentValidation.Results;
	using global::MediatR;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[UsedImplicitly]
	internal sealed class NotificationValidatingMediator : Mediator
	{
		private readonly IServiceProvider serviceProvider;

		/// <inheritdoc />
		public NotificationValidatingMediator(IServiceProvider serviceProvider) 
			: base(serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		/// <inheritdoc />
		public NotificationValidatingMediator(IServiceProvider serviceProvider, INotificationPublisher publisher) 
			: base(serviceProvider, publisher)
		{
			this.serviceProvider = serviceProvider;
		}

		/// <inheritdoc />
		protected override Task PublishCore(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
		{
			Type validatorType = typeof(IValidator<>).MakeGenericType(notification.GetType());
			IValidator[] validators = this.serviceProvider.GetServices(validatorType).Cast<IValidator>().ToArray();

			// Just publish the notifications if no validator exists.
			if(!validators.Any())
			{
				return base.PublishCore(handlerExecutors, notification, cancellationToken);
			}

			// Execute the available validators.
			ValidationFailure[] validationFailures = validators
				.Select(validator => validator.Validate(new ValidationContext<object>(notification)))
				.SelectMany(validationResult => validationResult.Errors)
				.Where(validationFailure => validationFailure is not null)
				.Distinct()
				.ToArray();

			if(validationFailures.Any())
			{
				ThrowOnValidationFailed(validationFailures);
			}

			return base.PublishCore(handlerExecutors, notification, cancellationToken);
		}

		private static void ThrowOnValidationFailed(ValidationFailure[] validationFailures)
		{
			if(validationFailures.Any())
			{
				throw new ValidationException(validationFailures);
			}
		}
	}
}
