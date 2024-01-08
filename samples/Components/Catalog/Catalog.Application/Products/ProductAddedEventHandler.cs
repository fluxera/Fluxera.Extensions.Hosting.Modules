namespace Catalog.Application.Products
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ProductAddedEventHandler : IEventHandler<ProductAddedEvent>
	{
		/// <inheritdoc />
		public Task Handle(ProductAddedEvent notification, CancellationToken cancellationToken)
		{
			Console.WriteLine(@$"PRODUCT ADDED: {notification.ProductId}");

			return Task.CompletedTask;
		}
	}
}
