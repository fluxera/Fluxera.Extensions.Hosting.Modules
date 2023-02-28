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
	using MassTransit;
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
		public Guid? TransactionId { get; private set; }

		/// <inheritdoc />
		public async Task<IClientSessionHandle> StartSession(CancellationToken cancellationToken)
		{
			return this.Session ??= await this.mongoContext.StartSessionAsync(cancellationToken);
		}

		/// <inheritdoc />
		public async Task BeginTransaction(CancellationToken cancellationToken)
		{
			await this.mongoContext.BeginTransactionAsync(cancellationToken);
			this.TransactionId = NewId.NextGuid();
		}

		/// <inheritdoc />
		public async Task CommitTransaction(CancellationToken cancellationToken)
		{
			await this.mongoContext.CommitTransactionAsync(cancellationToken);
			this.TransactionId = null;
		}

		/// <inheritdoc />
		public async Task AbortTransaction(CancellationToken cancellationToken)
		{
			await this.mongoContext.AbortTransactionAsync(cancellationToken);
			this.TransactionId = null;
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
