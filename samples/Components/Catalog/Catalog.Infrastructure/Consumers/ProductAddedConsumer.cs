namespace Catalog.Infrastructure.Consumers
{
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Products.Integration;
	using Fluxera.Extensions.Hosting.Modules.Infrastructure.Consumers;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A consumer implementation that consumes <see cref="ProductAdded" /> event messages.
	/// </summary>
	[UsedImplicitly]
	public sealed class ProductAddedConsumer : IIntegrationEventConsumer<ProductAdded>
	{
		/// <inheritdoc />
		public Task ConsumeAsync(ConsumeContext<ProductAdded> context)
		{
			// NOTE: Whatever needs to be done when this message is consumed goes into the application.

			return Task.CompletedTask;
		}
	}
}
