namespace Fluxera.Extensions.Hosting.Modules.Application.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Entity;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services;
	using Fluxera.Repository;
	using Fluxera.Utilities.Extensions;
	using global::AutoMapper;
	using global::AutoMapper.Extensions.ExpressionMapping;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for CRUD application services.
	/// </summary>
	/// <typeparam name="TDto"></typeparam>
	/// <typeparam name="TAggregateRoot"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public abstract class CrudApplicationService<TDto, TAggregateRoot, TKey> : ReadOnlyCrudApplicationService<TDto, TAggregateRoot, TKey>, ICrudApplicationService<TDto, TKey>
		where TDto : class, IEntityDto<TKey>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
		where TKey : notnull, IComparable<TKey>, IEquatable<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="CrudApplicationService{TDto, TAggregateRoot, TKey}" /> type.
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		protected CrudApplicationService(IRepository<TAggregateRoot, TKey> repository, IMapper mapper)
			: base(repository, mapper)
		{
			this.Repository = repository;
		}

		private IRepository<TAggregateRoot, TKey> Repository { get; }

		/// <inheritdoc />
		public virtual async Task AddAsync(TDto dto, CancellationToken cancellationToken = default)
		{
			TAggregateRoot item = this.Mapper.Map<TAggregateRoot>(dto);
			await this.Repository.AddAsync(item, cancellationToken);
			this.Mapper.Map(item, dto);
		}

		/// <inheritdoc />
		public virtual async Task AddRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			IList<TAggregateRoot> items = this.Mapper.Map<IList<TAggregateRoot>>(dtos);
			await this.Repository.AddRangeAsync(items, cancellationToken);
			this.Mapper.Map(items, dtos).AsReadOnly();
		}

		/// <inheritdoc />
		public virtual async Task UpdateAsync(TDto dto, CancellationToken cancellationToken = default)
		{
			TAggregateRoot item = this.Mapper.Map<TAggregateRoot>(dto);
			await this.Repository.UpdateAsync(item, cancellationToken);
			this.Mapper.Map(item, dto);
		}

		/// <inheritdoc />
		public virtual async Task UpdateRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			IList<TAggregateRoot> items = this.Mapper.Map<IList<TAggregateRoot>>(dtos);
			await this.Repository.UpdateRangeAsync(items, cancellationToken);
			this.Mapper.Map(items, dtos).AsReadOnly();
		}

		/// <inheritdoc />
		public virtual async Task RemoveAsync(TDto dto, CancellationToken cancellationToken = default)
		{
			TAggregateRoot item = this.Mapper.Map<TAggregateRoot>(dto);
			await this.Repository.RemoveAsync(item, cancellationToken);
			this.Mapper.Map(item, dto);
		}

		/// <inheritdoc />
		public virtual async Task RemoveAsync(TKey id, CancellationToken cancellationToken = default)
		{
			await this.Repository.RemoveAsync(id, cancellationToken);
		}

		/// <inheritdoc />
		public virtual async Task RemoveRangeAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
		{
			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
			await this.Repository.RemoveRangeAsync(mappedPredicate, cancellationToken);
		}

		/// <inheritdoc />
		public virtual async Task RemoveRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			IList<TAggregateRoot> items = this.Mapper.Map<IList<TAggregateRoot>>(dtos);
			await this.Repository.RemoveRangeAsync(items, cancellationToken);
			this.Mapper.Map(items, dtos);
		}
	}
}
