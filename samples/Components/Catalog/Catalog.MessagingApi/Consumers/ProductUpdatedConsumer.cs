﻿namespace Catalog.MessagingApi.Consumers
{
	using System;
	using System.Threading.Tasks;
	using Catalog.Domain.Shared.ProductAggregate.Messages;
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
			Console.WriteLine("CONSUMED PRODUCT UPDATED");

			return Task.CompletedTask;
		}
	}
}
