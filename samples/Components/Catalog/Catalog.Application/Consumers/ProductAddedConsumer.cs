namespace Catalog.Application.Consumers
{
	using System;
	using System.Threading.Tasks;
	using Catalog.Domain.Messages;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
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
			string applicationContext = context.Headers.Get(TransportHeaders.OriginApplicationHeaderName, string.Empty);
			Console.WriteLine(applicationContext);

			Console.WriteLine("CONSUMED PRODUCT ADDED");

			return Task.CompletedTask;
		}
	}
}
