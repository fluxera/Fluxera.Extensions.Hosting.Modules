namespace Catalog.MessagingApi.Consumers
{
	using System;
	using System.Threading.Tasks;
	using Catalog.Domain.Shared.ProductAggregate.Messages;
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
			Console.WriteLine("CONSUMED PRODUCT ADDED");

			return Task.CompletedTask;
		}
	}
}
