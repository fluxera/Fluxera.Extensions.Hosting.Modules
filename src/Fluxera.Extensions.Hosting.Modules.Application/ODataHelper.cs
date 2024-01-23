namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Entity;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Repository;
	using Fluxera.Repository.Query;
	using Fluxera.Utilities.Extensions;
	using global::AutoMapper;
	using global::AutoMapper.Extensions.ExpressionMapping;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ODataHelper<TDto, TKey, TAggregateRoot>
		where TDto : class, IEntityDto<TKey>
		where TKey : IComparable<TKey>, IEquatable<TKey>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
	{
		private readonly IRepository<TAggregateRoot, TKey> Repository;
		private readonly IMapper Mapper;

		/// <summary>
		///		Initializes a new instance of the <see cref="ODataHelper{TDto, TKey, TAggregateRoot}"/> type.
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		public ODataHelper(IRepository<TAggregateRoot, TKey> repository, IMapper mapper)
		{
			this.Repository = repository;
			this.Mapper = mapper;
		}

		public async Task AddAsync(TDto dto, CancellationToken cancellationToken)
		{
			TAggregateRoot item = this.Mapper.Map<TAggregateRoot>(dto);
			await this.Repository.AddAsync(item, cancellationToken).ConfigureAwait(false);
			this.Mapper.Map(item, dto);
		}

		public async Task AddRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			IList<TAggregateRoot> items = this.Mapper.Map<IList<TAggregateRoot>>(dtos);
			await this.Repository.AddRangeAsync(items, cancellationToken).ConfigureAwait(false);
			this.Mapper.Map(items, dtos);
		}

		public async Task UpdateAsync(TDto dto, CancellationToken cancellationToken)
		{
			TAggregateRoot item = this.Mapper.Map<TAggregateRoot>(dto);
			await this.Repository.UpdateAsync(item, cancellationToken).ConfigureAwait(false);
			this.Mapper.Map(item, dto);
		}

		public async Task UpdateRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			IList<TAggregateRoot> items = this.Mapper.Map<IList<TAggregateRoot>>(dtos);
			await this.Repository.UpdateRangeAsync(items, cancellationToken).ConfigureAwait(false);
			this.Mapper.Map(items, dtos);
		}

		public async Task DeleteAsync(TDto dto, CancellationToken cancellationToken = default)
		{
			TAggregateRoot item = this.Mapper.Map<TAggregateRoot>(dto);
			await this.Repository.RemoveAsync(item, cancellationToken).ConfigureAwait(false);
			this.Mapper.Map(item, dto);
		}

		public async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
		{
			await this.Repository.RemoveAsync(id, cancellationToken).ConfigureAwait(false);
		}

		public async Task DeleteRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			IList<TAggregateRoot> items = this.Mapper.Map<IList<TAggregateRoot>>(dtos);
			await this.Repository.RemoveRangeAsync(items, cancellationToken).ConfigureAwait(false);
			this.Mapper.Map(items, dtos);
		}

		public async Task DeleteRangeAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			await this.Repository.RemoveRangeAsync(mappedPredicate, cancellationToken).ConfigureAwait(false);
		}

		public async Task<TDto> GetAsync(TKey id, CancellationToken cancellationToken)
		{
			TAggregateRoot result = await this.Repository.GetAsync(id, cancellationToken).ConfigureAwait(false);
			TDto resultDto = this.Mapper.Map<TDto>(result);
			return resultDto;
		}

		public async Task<TResult> GetAsync<TResult>(TKey id, Expression<Func<TDto, TResult>> selector, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);
			return await this.Repository.GetAsync(id, mappedSelector, cancellationToken).ConfigureAwait(false);
		}

		public async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken)
		{
			return await this.Repository.ExistsAsync(id, cancellationToken).ConfigureAwait(false);
		}

		public async Task<bool> ExistsAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			return await this.Repository.ExistsAsync(mappedPredicate, cancellationToken).ConfigureAwait(false);
		}

		public async Task<long> CountAsync(CancellationToken cancellationToken = default)
		{
			return await this.CountAsync(x => true, cancellationToken).ConfigureAwait(false);
		}

		public async Task<long> CountAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			return await this.Repository.CountAsync(mappedPredicate, cancellationToken).ConfigureAwait(false);
		}

		public async Task<TDto> FindOneAsync(
			Expression<Func<TDto, bool>> predicate,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			IQueryOptions<TAggregateRoot> mappedQueryOptions = this.MapQueryOptions(queryOptions);

			TAggregateRoot item = await this.Repository.FindOneAsync(mappedPredicate, mappedQueryOptions, cancellationToken).ConfigureAwait(false);
			TDto dto = this.Mapper.Map<TDto>(item);
			return dto;
		}

		public async Task<TResult> FindOneAsync<TResult>(
			Expression<Func<TDto, bool>> predicate,
			Expression<Func<TDto, TResult>> selector,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);
			IQueryOptions<TAggregateRoot> mappedQueryOptions = this.MapQueryOptions(queryOptions);

			return await this.Repository.FindOneAsync(mappedPredicate, mappedSelector, mappedQueryOptions, cancellationToken).ConfigureAwait(false);

		}

		public async Task<IReadOnlyCollection<TDto>> FindManyAsync(
			Expression<Func<TDto, bool>> predicate,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			IQueryOptions<TAggregateRoot> mappedQueryOptions = this.MapQueryOptions(queryOptions);

			IReadOnlyCollection<TAggregateRoot> items = await this.Repository.FindManyAsync(mappedPredicate, mappedQueryOptions, cancellationToken)
				.ConfigureAwait(false);
			IList<TDto> dtos = this.Mapper.Map<IList<TDto>>(items);
			return dtos.AsReadOnly();
		}

		public async Task<IReadOnlyCollection<TResult>> FindManyAsync<TResult>(
			Expression<Func<TDto, bool>> predicate,
			Expression<Func<TDto, TResult>> selector,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);
			IQueryOptions<TAggregateRoot> mappedQueryOptions = this.MapQueryOptions(queryOptions);

			IReadOnlyCollection<TResult> results = await this.Repository.FindManyAsync(mappedPredicate, mappedSelector, mappedQueryOptions, cancellationToken)
				.ConfigureAwait(false);
			return results;
		}

		// TODO: Aggregate methods

		private IQueryOptions<TAggregateRoot> MapQueryOptions(IQueryOptions<TDto> dtoQueryOptions)
		{
			IQueryOptions<TAggregateRoot> mappedQueryOptions = QueryOptionsBuilder<TAggregateRoot>.Empty();

			// TODO

			return mappedQueryOptions;
		}

		//private IQueryOptions<TAggregateRoot> MapQueryOptions(IQueryOptions<TDto> dtoQueryOptions)
		//{
		//	IQueryOptions<TAggregateRoot> queryOptions = QueryOptions.CreateFor<TAggregateRoot>();
		//	QueryOptions<TAggregateRoot> options = (QueryOptions<TAggregateRoot>)queryOptions;

		//	if(options.HasPagingOptions())
		//	{
		//		if(options.PagingOptions is PagingOptions<TAggregateRoot> pagingOptions)
		//		{
		//			queryOptions.Paging(pagingOptions.PageNumber, pagingOptions.PageSize);
		//		}
		//	}

		//	if(options.HasSkipTakeOptions())
		//	{
		//		if(options.SkipTakeOptions != null)
		//		{
		//			if(options.SkipTakeOptions.Skip.HasValue)
		//			{
		//				queryOptions.Skip(options.SkipTakeOptions.Skip.Value);
		//			}

		//			if(options.SkipTakeOptions.Take.HasValue)
		//			{
		//				queryOptions.Skip(options.SkipTakeOptions.Take.Value);
		//			}
		//		}
		//	}

		//	if(options.HasOrderByOptions())
		//	{
		//		OrderByOptions<TAggregateRoot>
		//			orderByOptions = options.OrderByOptions as OrderByOptions<TAggregateRoot>;
		//		OrderByExpressionContainer<TAggregateRoot> orderByBy = orderByOptions?.OrderByExpression;
		//		if(orderByBy != null)
		//		{
		//			Expression<Func<TAggregateRoot, object>> mappedOrderByExpression =
		//				this.Mapper.MapExpression<Expression<Func<TAggregateRoot, object>>>(orderByBy.SortExpression);

		//			IThenByOptions<TAggregateRoot> thenByOptions = orderByBy.IsDescending
		//				? queryOptions.OrderByDescending(mappedOrderByExpression)
		//				: queryOptions.OrderBy(mappedOrderByExpression);

		//			if(orderByOptions.ThenByExpressions != null)
		//			{
		//				foreach(OrderByExpressionContainer<TAggregateRoot> thenBy in orderByOptions.ThenByExpressions)
		//				{
		//					Expression<Func<TAggregateRoot, object>> mappedThenByExpression =
		//						this.Mapper.MapExpression<Expression<Func<TAggregateRoot, object>>>(
		//							thenBy.SortExpression);

		//					thenByOptions = thenBy.IsDescending
		//						? thenByOptions.ThenBy(mappedThenByExpression)
		//						: thenByOptions.ThenByDescending(mappedThenByExpression);
		//				}
		//			}
		//		}
		//	}

		//	return queryOptions;
		//}
	}
}
