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

	/// <summary>
	///     A helper class to execute a datasource from DTOs.
	/// </summary>
	/// <typeparam name="TDto"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TAggregateRoot"></typeparam>
	[PublicAPI]
	public sealed class ODataHelper<TDto, TKey, TAggregateRoot>
		where TDto : class, IEntityDto<TKey>
		where TKey : IComparable<TKey>, IEquatable<TKey>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
	{
		private readonly IRepository<TAggregateRoot, TKey> repository;
		private readonly IMapper mapper;

		/// <summary>
		///     Initializes a new instance of the <see cref="ODataHelper{TDto, TKey, TAggregateRoot}" /> type.
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		public ODataHelper(IRepository<TAggregateRoot, TKey> repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <summary>
		///     Adds the given item.
		/// </summary>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task AddAsync(TDto dto, CancellationToken cancellationToken)
		{
			TAggregateRoot item = this.mapper.Map<TAggregateRoot>(dto);
			await this.repository.AddAsync(item, cancellationToken).ConfigureAwait(false);
			this.mapper.Map(item, dto);
		}

		/// <summary>
		///     Adds the given items.
		/// </summary>
		/// <param name="dtos"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task AddRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			IList<TAggregateRoot> items = this.mapper.Map<IList<TAggregateRoot>>(dtos);
			await this.repository.AddRangeAsync(items, cancellationToken).ConfigureAwait(false);
			this.mapper.Map(items, dtos);
		}

		/// <summary>
		///     Updates the given item.
		/// </summary>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task UpdateAsync(TDto dto, CancellationToken cancellationToken)
		{
			TAggregateRoot item = this.mapper.Map<TAggregateRoot>(dto);
			await this.repository.UpdateAsync(item, cancellationToken).ConfigureAwait(false);
			this.mapper.Map(item, dto);
		}

		/// <summary>
		///     Updates the given items.
		/// </summary>
		/// <param name="dtos"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task UpdateRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			IList<TAggregateRoot> items = this.mapper.Map<IList<TAggregateRoot>>(dtos);
			await this.repository.UpdateRangeAsync(items, cancellationToken).ConfigureAwait(false);
			this.mapper.Map(items, dtos);
		}

		/// <summary>
		///     Deletes the given item.
		/// </summary>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task DeleteAsync(TDto dto, CancellationToken cancellationToken = default)
		{
			TAggregateRoot item = this.mapper.Map<TAggregateRoot>(dto);
			await this.repository.RemoveAsync(item, cancellationToken).ConfigureAwait(false);
			this.mapper.Map(item, dto);
		}

		/// <summary>
		///     Deletes an item by ID.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
		{
			await this.repository.RemoveAsync(id, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Deletes the given items.
		/// </summary>
		/// <param name="dtos"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task DeleteRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			IList<TAggregateRoot> items = this.mapper.Map<IList<TAggregateRoot>>(dtos);
			await this.repository.RemoveRangeAsync(items, cancellationToken).ConfigureAwait(false);
			this.mapper.Map(items, dtos);
			this.mapper.Map(items, dtos);
		}

		/// <summary>
		///     Deletes the items that satisfy the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task DeleteRangeAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			await this.repository.RemoveRangeAsync(mappedPredicate, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Gets an item by ID.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TDto> GetAsync(TKey id, CancellationToken cancellationToken)
		{
			TAggregateRoot result = await this.repository.GetAsync(id, cancellationToken).ConfigureAwait(false);
			return this.mapper.Map<TDto>(result);
		}

		/// <summary>
		///     Gets an item by ID and returns the values selected by the selector.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="id"></param>
		/// <param name="selector"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TResult> GetAsync<TResult>(TKey id, Expression<Func<TDto, TResult>> selector, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);
			return await this.repository.GetAsync(id, mappedSelector, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Checks if the item with the given ID exists.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken)
		{
			return await this.repository.ExistsAsync(id, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Checks if items satisfying by the given predicate exist.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> ExistsAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			return await this.repository.ExistsAsync(mappedPredicate, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Gets the total count of items.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<long> CountAsync(CancellationToken cancellationToken = default)
		{
			return await this.CountAsync(x => true, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Gets the count of items that satisfy the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<long> CountAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			return await this.repository.CountAsync(mappedPredicate, cancellationToken).ConfigureAwait(false);
		}

		public async Task<TDto> FindOneAsync(
			Expression<Func<TDto, bool>> predicate,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			IQueryOptions<TAggregateRoot> mappedQueryOptions = this.MapQueryOptions(queryOptions);

			TAggregateRoot item = await this.repository.FindOneAsync(mappedPredicate, mappedQueryOptions, cancellationToken).ConfigureAwait(false);
			return this.mapper.Map<TDto>(item);
		}

		public async Task<TResult> FindOneAsync<TResult>(
			Expression<Func<TDto, bool>> predicate,
			Expression<Func<TDto, TResult>> selector,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);
			IQueryOptions<TAggregateRoot> mappedQueryOptions = this.MapQueryOptions(queryOptions);

			return await this.repository.FindOneAsync(mappedPredicate, mappedSelector, mappedQueryOptions, cancellationToken).ConfigureAwait(false);
		}

		public async Task<IReadOnlyCollection<TDto>> FindManyAsync(
			Expression<Func<TDto, bool>> predicate,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			IQueryOptions<TAggregateRoot> mappedQueryOptions = this.MapQueryOptions(queryOptions);

			IReadOnlyCollection<TAggregateRoot> items = await this.repository.FindManyAsync(mappedPredicate, mappedQueryOptions, cancellationToken)
				.ConfigureAwait(false);
			return this.mapper.Map<IList<TDto>>(items).AsReadOnly();
		}

		public async Task<IReadOnlyCollection<TResult>> FindManyAsync<TResult>(
			Expression<Func<TDto, bool>> predicate,
			Expression<Func<TDto, TResult>> selector,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);
			IQueryOptions<TAggregateRoot> mappedQueryOptions = this.MapQueryOptions(queryOptions);

			return await this.repository.FindManyAsync(mappedPredicate, mappedSelector, mappedQueryOptions, cancellationToken).ConfigureAwait(false);
		}

		// TODO: Aggregate methods

		private IQueryOptions<TAggregateRoot> MapQueryOptions(IQueryOptions<TDto> queryOptions)
		{
			return queryOptions.Convert(this.mapper.MapExpression<Expression<Func<TAggregateRoot, object>>>);
		}
	}
}
