//namespace Fluxera.Extensions.Hosting.Modules.Application.Services
//{
//	using System;
//	using System.Collections.Generic;
//	using System.Linq.Expressions;
//	using System.Threading;
//	using System.Threading.Tasks;
//	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
//	using Fluxera.Repository.Query;
//	using global::AutoMapper;
//	using global::AutoMapper.Extensions.ExpressionMapping;
//	using JetBrains.Annotations;

//	[PublicAPI]
//	public abstract class ReadOnlyCrudApplicationService<TDto, TAggregateRoot> : ApplicationService,
//		IReadOnlyCrudApplicationService<TDto>
//		where TDto : class, IEntityDto
//		where TAggregateRoot : AggregateRoot<TAggregateRoot>
//	{
//		protected ReadOnlyCrudApplicationService(
//			IReadOnlyRepository<TAggregateRoot> repository,
//			IMapper mapper)
//		{
//			this.Repository = repository;
//			this.Mapper = mapper;
//		}

//		private IReadOnlyRepository<TAggregateRoot> Repository { get; }

//		protected IMapper Mapper { get; }

//		/// <inheritdoc />
//		public virtual async Task<TDto> GetAsync(string id, CancellationToken cancellationToken = default)
//		{
//			TAggregateRoot result = await this.Repository.GetAsync(id, cancellationToken).ConfigureAwait(false);
//			TDto resultDto = this.Mapper.Map<TDto>(result);
//			return resultDto;
//		}

//		/// <inheritdoc />
//		public virtual async Task<TResult> GetAsync<TResult>(string id, Expression<Func<TDto, TResult>> selector,
//			CancellationToken cancellationToken = default)
//		{
//			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);

//			return await this.Repository.GetAsync(id, mappedSelector, cancellationToken).ConfigureAwait(false);
//		}

//		/// <inheritdoc />
//		public virtual async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
//		{
//			return await this.Repository.ExistsAsync(id, cancellationToken).ConfigureAwait(false);
//		}

//		/// <inheritdoc />
//		public virtual async Task<bool> ExistsAsync(Expression<Func<TDto, bool>> predicate,
//			CancellationToken cancellationToken = default)
//		{
//			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);

//			return await this.Repository.ExistsAsync(mappedPredicate, cancellationToken).ConfigureAwait(false);
//		}

//		/// <inheritdoc />
//		public virtual async Task<long> CountAsync(CancellationToken cancellationToken = default)
//		{
//			return await this.CountAsync(x => true, cancellationToken).ConfigureAwait(false);
//		}

//		/// <inheritdoc />
//		public virtual async Task<long> CountAsync(Expression<Func<TDto, bool>> predicate,
//			CancellationToken cancellationToken = default)
//		{
//			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);

//			return await this.Repository.CountAsync(mappedPredicate, cancellationToken).ConfigureAwait(false);
//		}

//		/// <inheritdoc />
//		public virtual async Task<TDto> FindOneAsync(Expression<Func<TDto, bool>> predicate,
//			IQueryOptions<TDto> queryOptions = null, CancellationToken cancellationToken = default)
//		{
//			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
//			IQueryOptions<TAggregateRoot> mappedQueryOptions = this.MapQueryOptions(queryOptions);

//			TAggregateRoot item = await this.Repository.FindOneAsync(mappedPredicate, mappedQueryOptions, cancellationToken).ConfigureAwait(false);
//			TDto dto = this.Mapper.Map<TDto>(item);
//			return dto;
//		}

//		/// <inheritdoc />
//		public virtual async Task<TResult> FindOneAsync<TResult>(Expression<Func<TDto, bool>> predicate,
//			Expression<Func<TDto, TResult>> selector, IQueryOptions<TDto> queryOptions = null,
//			CancellationToken cancellationToken = default)
//		{
//			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
//			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);
//			IQueryOptions<TAggregateRoot> mappedQueryOptions = this.MapQueryOptions(queryOptions);

//			return await this.Repository.FindOneAsync(mappedPredicate, mappedSelector, mappedQueryOptions, cancellationToken).ConfigureAwait(false);
//		}

//		/// <inheritdoc />
//		public virtual async Task<IReadOnlyList<TDto>> FindManyAsync(Expression<Func<TDto, bool>> predicate,
//			IQueryOptions<TDto> queryOptions, CancellationToken cancellationToken = default)
//		{
//			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
//			IQueryOptions<TAggregateRoot> mappedQueryOptions = this.MapQueryOptions(queryOptions);

//			IReadOnlyList<TAggregateRoot> items = await this.Repository.FindManyAsync(mappedPredicate, mappedQueryOptions, cancellationToken).ConfigureAwait(false);
//			IList<TDto> dtos = this.Mapper.Map<IList<TDto>>(items);
//			return dtos.ToReadOnly();
//		}

//		/// <inheritdoc />
//		public virtual async Task<IReadOnlyList<TResult>> FindManyAsync<TResult>(Expression<Func<TDto, bool>> predicate,
//			Expression<Func<TDto, TResult>> selector, IQueryOptions<TDto> queryOptions = null,
//			CancellationToken cancellationToken = default)
//		{
//			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
//			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);

//			IReadOnlyList<TResult> results = await this.Repository.FindManyAsync(mappedPredicate, mappedSelector, null, cancellationToken).ConfigureAwait(false);
//			return results;
//		}

//		private IQueryOptions<TAggregateRoot> MapQueryOptions(IQueryOptions<TDto> dtoQueryOptions)
//		{
//			IQueryOptions<TAggregateRoot> queryOptions = QueryOptions.CreateFor<TAggregateRoot>();
//			QueryOptions<TAggregateRoot> options = (QueryOptions<TAggregateRoot>)queryOptions;

//			if(options.HasPagingOptions())
//			{
//				if(options.PagingOptions is PagingOptions<TAggregateRoot> pagingOptions)
//				{
//					queryOptions.Paging(pagingOptions.PageNumber, pagingOptions.PageSize);
//				}
//			}

//			if(options.HasSkipTakeOptions())
//			{
//				if(options.SkipTakeOptions != null)
//				{
//					if(options.SkipTakeOptions.Skip.HasValue)
//					{
//						queryOptions.Skip(options.SkipTakeOptions.Skip.Value);
//					}

//					if(options.SkipTakeOptions.Take.HasValue)
//					{
//						queryOptions.Skip(options.SkipTakeOptions.Take.Value);
//					}
//				}
//			}

//			if(options.HasOrderByOptions())
//			{
//				OrderByOptions<TAggregateRoot>
//					orderByOptions = options.OrderByOptions as OrderByOptions<TAggregateRoot>;
//				OrderByExpressionContainer<TAggregateRoot> orderByBy = orderByOptions?.OrderByExpression;
//				if(orderByBy != null)
//				{
//					Expression<Func<TAggregateRoot, object>> mappedOrderByExpression =
//						this.Mapper.MapExpression<Expression<Func<TAggregateRoot, object>>>(orderByBy.SortExpression);

//					IThenByOptions<TAggregateRoot> thenByOptions = orderByBy.IsDescending
//						? queryOptions.OrderByDescending(mappedOrderByExpression)
//						: queryOptions.OrderBy(mappedOrderByExpression);

//					if(orderByOptions.ThenByExpressions != null)
//					{
//						foreach(OrderByExpressionContainer<TAggregateRoot> thenBy in orderByOptions.ThenByExpressions)
//						{
//							Expression<Func<TAggregateRoot, object>> mappedThenByExpression =
//								this.Mapper.MapExpression<Expression<Func<TAggregateRoot, object>>>(
//									thenBy.SortExpression);

//							thenByOptions = thenBy.IsDescending
//								? thenByOptions.ThenBy(mappedThenByExpression)
//								: thenByOptions.ThenByDescending(mappedThenByExpression);
//						}
//					}
//				}
//			}

//			return queryOptions;
//		}
//	}
//}


