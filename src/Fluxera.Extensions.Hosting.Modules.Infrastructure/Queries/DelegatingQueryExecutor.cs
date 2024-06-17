namespace Fluxera.Extensions.Hosting.Modules.Infrastructure.Queries
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Queries;
	using Fluxera.Queries;
	using Fluxera.Queries.Options;
	using Fluxera.Results;
	using global::MediatR;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	[UsedImplicitly]
	internal sealed class DelegatingQueryExecutor<TDto, TKey> : QueryExecutorBase<TDto, TKey>
		where TDto : class
		where TKey : IComparable<TKey>, IEquatable<TKey>
	{
		private readonly ISender sender;
		private readonly DataQueriesOptions options;

		public DelegatingQueryExecutor(ISender sender, IOptions<DataQueriesOptions> options)
		{
			this.sender = sender;
			this.options = options.Value;
		}

		/// <inheritdoc />
		public override async Task<QueryResult> ExecuteFindManyAsync(QueryOptions queryOptions, CancellationToken cancellationToken = default)
		{
			EntitySetOptions entitySetOptions = this.options.GetOptionsByType(typeof(TDto));

			Type findQueryType = (Type)entitySetOptions.GetMetadata("FindQueryType");
			if(findQueryType is null)
			{
				throw new InvalidOperationException($"No FindQuery was configured for the entity set {entitySetOptions.Name}.");
			}

			IFindQuery query = (IFindQuery)Activator.CreateInstance(findQueryType, [queryOptions]);
			if(query is null)
			{
				throw new InvalidOperationException($"An instance of the FindQuery type {findQueryType} could not be created.");
			}

			Result<QueryResult> result = await this.sender.Send(query, cancellationToken);
			return result.Value;
		}

		/// <inheritdoc />
		public override async Task<SingleResult> ExecuteGetAsync(TKey id, QueryOptions queryOptions, CancellationToken cancellationToken = default)
		{
			EntitySetOptions entitySetOptions = this.options.GetOptionsByType(typeof(TDto));

			Type getQueryType = (Type)entitySetOptions.GetMetadata("GetQueryType");
			if(getQueryType is null)
			{
				throw new InvalidOperationException($"No GetQuery was configured for the entity set {entitySetOptions.Name}.");
			}

			IGetQuery query = (IGetQuery)Activator.CreateInstance(getQueryType, [id, queryOptions]);
			if(query is null)
			{
				throw new InvalidOperationException($"An instance of the GetQuery type {getQueryType} could not be created.");
			}

			Result<SingleResult> result = await this.sender.Send(query, cancellationToken);
			return result.Value;
		}

		/// <inheritdoc />
		public override async Task<long> ExecuteCountAsync(QueryOptions queryOptions, CancellationToken cancellationToken = default)
		{
			EntitySetOptions entitySetOptions = this.options.GetOptionsByType(typeof(TDto));

			Type countQueryType = (Type)entitySetOptions.GetMetadata("CountQueryType");
			if(countQueryType is null)
			{
				throw new InvalidOperationException($"No CountQuery was configured for the entity set {entitySetOptions.Name}.");
			}

			ICountQuery query = (ICountQuery)Activator.CreateInstance(countQueryType, [queryOptions]);
			if(query is null)
			{
				throw new InvalidOperationException($"An instance of the CountQuery type {countQueryType} could not be created.");
			}

			Result<long> result = await this.sender.Send(query, cancellationToken);
			return result.Value;
		}
	}
}
