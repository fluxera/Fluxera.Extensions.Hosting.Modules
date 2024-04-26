namespace Catalog.Infrastructure.Consumers
{
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Products.Integration;
	using Fluxera.Extensions.Hosting.Modules.Infrastructure.Consumers;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A consumer implementation that consumes <see cref="ProductUpdated" /> event messages.
	/// </summary>
	[UsedImplicitly]
	public sealed class ProductUpdatedConsumer : IIntegrationEventConsumer<ProductUpdated>
	{
		/// <inheritdoc />
		public Task ConsumeAsync(ConsumeContext<ProductUpdated> context)
		{
			// NOTE: Whatever needs to be done when this message is consumed goes into the application.

			return Task.CompletedTask;
		}
	}
}
