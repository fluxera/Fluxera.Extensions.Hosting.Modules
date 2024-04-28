namespace Fluxera.Extensions.Hosting.Modules.Application.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Application.Validation;
	using Fluxera.Extensions.Hosting.Modules.MediatR;
	using global::MediatR;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[UsedImplicitly]
	internal sealed class MediatrContributor : IMediatrContributor
	{
		/// <inheritdoc />
		public void Configure(IServiceConfigurationContext context, MediatRServiceConfiguration configuration)
		{
			// Use a specialized mediator implementation that also validates notifications.
			configuration.MediatorImplementationType = typeof(NotificationValidatingMediator);

			// Add the validation pipeline behavior for MediatR.
			context.Log("AddPipelineBehavior(ValidationPipelineBehavior)", services =>
			{
				services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
			});
		}
	}
}
