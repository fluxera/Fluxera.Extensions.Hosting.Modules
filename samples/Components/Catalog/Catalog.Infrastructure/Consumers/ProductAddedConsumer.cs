namespace Catalog.Infrastructure.Consumers
{
	using System.Threading.Tasks;
	using Catalog.Domain.Messages.Products;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A consumer implementation that consumes <see cref="ProductAdded" /> event messages.
	/// </summary>
	[UsedImplicitly]
	public sealed class ProductAddedConsumer : IConsumer<ProductAdded>
	{
		/// <inheritdoc />
		public Task Consume(ConsumeContext<ProductAdded> context)
		{
			// NOTE: Whatever needs to be done when this message is consumed goes into the application.

			return Task.CompletedTask;
		}
	}
}
