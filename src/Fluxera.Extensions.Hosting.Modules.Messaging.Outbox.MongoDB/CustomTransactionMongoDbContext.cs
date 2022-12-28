namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Guards;
	using Fluxera.Repository;
	using Fluxera.Repository.MongoDB;
	using global::MongoDB.Driver;
	using JetBrains.Annotations;
	using MassTransit.MongoDbIntegration;
	using Microsoft.Extensions.Options;

	[UsedImplicitly]
	internal sealed class CustomTransactionMongoDbContext : MongoDbContext
	{
		private readonly MongoContext mongoContext;

		public CustomTransactionMongoDbContext(MongoContextProvider contextProvider, IOptions<MessagingOutboxModuleOptions> options)
		{
			contextProvider = Guard.Against.Null(contextProvider);
			this.mongoContext = contextProvider.GetContextFor((RepositoryName)options.Value.RepositoryName);
		}

		/// <inheritdoc />
		public IClientSessionHandle Session { get; private set; }

		/// <inheritdoc />
		public async Task<IClientSessionHandle> StartSession(CancellationToken cancellationToken)
		{
			return this.Session ??= await this.mongoContext.StartSessionAsync(cancellationToken);
		}

		/// <inheritdoc />
		public Task BeginTransaction(CancellationToken cancellationToken)
		{
			return this.mongoContext.BeginTransactionAsync(cancellationToken);
		}

		/// <inheritdoc />
		public Task CommitTransaction(CancellationToken cancellationToken)
		{
			return this.mongoContext.CommitTransactionAsync(cancellationToken);
		}

		/// <inheritdoc />
		public Task AbortTransaction(CancellationToken cancellationToken)
		{
			return this.mongoContext.AbortTransactionAsync(cancellationToken);
		}

		/// <inheritdoc />
		public MongoDbCollectionContext<T> GetCollection<T>()
		{
			IMongoCollection<T> collection = this.mongoContext.GetCollection<T>();
			return new TransactionMongoDbCollectionContext<T>(this, collection);
		}

		/// <inheritdoc />
		public void Dispose()
		{
			(this.mongoContext as IDisposable)?.Dispose();
		}
	}
}
