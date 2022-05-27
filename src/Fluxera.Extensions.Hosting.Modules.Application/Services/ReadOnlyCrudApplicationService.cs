namespace Fluxera.Extensions.Hosting.Modules.Application.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Entity;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Query;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services;
	using Fluxera.Repository;
	using Fluxera.Utilities.Extensions;
	using global::AutoMapper;
	using global::AutoMapper.Extensions.ExpressionMapping;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for read-only application services.
	/// </summary>
	/// <typeparam name="TDto"></typeparam>
	/// <typeparam name="TAggregateRoot"></typeparam>
	[PublicAPI]
	public abstract class ReadOnlyCrudApplicationService<TDto, TAggregateRoot> : IReadOnlyCrudApplicationService<TDto>
		where TDto : class, IEntityDto
		where TAggregateRoot : AggregateRoot<TAggregateRoot, string>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ReadOnlyCrudApplicationService{TDto, TAggregateRoot}" /> type.
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		protected ReadOnlyCrudApplicationService(IReadOnlyRepository<TAggregateRoot, string> repository, IMapper mapper)
		{
			this.Repository = repository;
			this.Mapper = mapper;
		}

		private IReadOnlyRepository<TAggregateRoot, string> Repository { get; }

		/// <summary>
		///     Gets the mapper.
		/// </summary>
		protected IMapper Mapper { get; }

		/// <inheritdoc />
		public virtual async Task<TDto> GetAsync(string id, CancellationToken cancellationToken = default)
		{
			TAggregateRoot result = await this.Repository.GetAsync(id, cancellationToken).ConfigureAwait(false);
			TDto resultDto = this.Mapper.Map<TDto>(result);
			return resultDto;
		}

		/// <inheritdoc />
		public virtual async Task<TResult> GetAsync<TResult>(string id, Expression<Func<TDto, TResult>> selector, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);
			return await this.Repository.GetAsync(id, mappedSelector, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public virtual async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
		{
			return await this.Repository.ExistsAsync(id, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public virtual async Task<bool> ExistsAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			return await this.Repository.ExistsAsync(mappedPredicate, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public virtual async Task<long> CountAsync(CancellationToken cancellationToken = default)
		{
			return await this.CountAsync(x => true, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public virtual async Task<long> CountAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			return await this.Repository.CountAsync(mappedPredicate, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public virtual async Task<TDto> FindOneAsync(Expression<Func<TDto, bool>> predicate, IQueryOptions<TDto> queryOptions = null, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			Repository.Query.IQueryOptions<TAggregateRoot> mappedQueryOptions = queryOptions.MapQueryOptions<TDto, TAggregateRoot>(this.Mapper);

			TAggregateRoot item = await this.Repository.FindOneAsync(mappedPredicate, mappedQueryOptions, cancellationToken).ConfigureAwait(false);
			TDto dto = this.Mapper.Map<TDto>(item);
			return dto;
		}

		/// <inheritdoc />
		public virtual async Task<TResult> FindOneAsync<TResult>(Expression<Func<TDto, bool>> predicate, Expression<Func<TDto, TResult>> selector, IQueryOptions<TDto> queryOptions = null, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);
			Repository.Query.IQueryOptions<TAggregateRoot> mappedQueryOptions = queryOptions.MapQueryOptions<TDto, TAggregateRoot>(this.Mapper);

			return await this.Repository.FindOneAsync(mappedPredicate, mappedSelector, mappedQueryOptions, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public virtual async Task<IReadOnlyCollection<TDto>> FindManyAsync(Expression<Func<TDto, bool>> predicate, IQueryOptions<TDto> queryOptions, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			Repository.Query.IQueryOptions<TAggregateRoot> mappedQueryOptions = queryOptions.MapQueryOptions<TDto, TAggregateRoot>(this.Mapper);

			IReadOnlyCollection<TAggregateRoot> items = await this.Repository.FindManyAsync(mappedPredicate, mappedQueryOptions, cancellationToken).ConfigureAwait(false);
			IList<TDto> dtos = this.Mapper.Map<IList<TDto>>(items);
			return dtos.AsReadOnly();
		}

		/// <inheritdoc />
		public virtual async Task<IReadOnlyCollection<TResult>> FindManyAsync<TResult>(Expression<Func<TDto, bool>> predicate, Expression<Func<TDto, TResult>> selector, IQueryOptions<TDto> queryOptions = null, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			Expression<Func<TAggregateRoot, TResult>> mappedSelector = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, TResult>>>(selector);

			IReadOnlyCollection<TResult> results = await this.Repository.FindManyAsync(mappedPredicate, mappedSelector, null, cancellationToken).ConfigureAwait(false);
			return results;
		}
	}
}
