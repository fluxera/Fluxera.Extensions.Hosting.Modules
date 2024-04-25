namespace Catalog.Infrastructure.Consumers
{
	using System.Threading.Tasks;
	using Catalog.Domain.Products.DomainEvents;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A consumer implementation that consumes <see cref="ProductUpdated" /> event messages.
	/// </summary>
	[UsedImplicitly]
	public sealed class ProductUpdatedConsumer : IConsumer<ProductUpdated>
	{
		/// <inheritdoc />
		public Task Consume(ConsumeContext<ProductUpdated> context)
		{
			// NOTE: Whatever needs to be done when this message is consumed goes into the application.

			return Task.CompletedTask;
		}
	}
}
