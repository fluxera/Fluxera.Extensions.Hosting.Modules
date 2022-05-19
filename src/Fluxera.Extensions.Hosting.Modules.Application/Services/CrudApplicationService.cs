//namespace Fluxera.Extensions.Hosting.Modules.Application.Services
//{
//	using System;
//	using System.Collections.Generic;
//	using System.Linq.Expressions;
//	using System.Threading;
//	using System.Threading.Tasks;
//	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
//	using global::AutoMapper;
//	using global::AutoMapper.Extensions.ExpressionMapping;
//	using JetBrains.Annotations;

//	[PublicAPI]
//	public abstract class CrudApplicationService<TDto, TAggregateRoot> : ReadOnlyCrudApplicationService<TDto, TAggregateRoot>, ICrudApplicationService<TDto>
//		where TDto : class, IEntityDto
//		where TAggregateRoot : AggregateRoot<TAggregateRoot>
//	{
//		protected CrudApplicationService(
//			IRepository<TAggregateRoot> repository,
//			IMapper mapper)
//			: base(repository, mapper)
//		{
//			this.Repository = repository;
//		}

//		private IRepository<TAggregateRoot> Repository { get; }

//		/// <inheritdoc />
//		public virtual async Task AddAsync(TDto dto, CancellationToken cancellationToken = default)
//		{
//			TAggregateRoot item = this.Mapper.Map<TAggregateRoot>(dto);
//			await this.Repository.AddAsync(item, cancellationToken);
//			this.Mapper.Map(item, dto);
//		}

//		/// <inheritdoc />
//		public virtual async Task AddAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
//		{
//			IList<TAggregateRoot> items = this.Mapper.Map<IList<TAggregateRoot>>(dtos);
//			await this.Repository.AddAsync(items, cancellationToken);
//			this.Mapper.Map(items, dtos).ToReadOnly();
//		}

//		/// <inheritdoc />
//		public virtual async Task UpdateAsync(TDto dto, CancellationToken cancellationToken = default)
//		{
//			TAggregateRoot item = this.Mapper.Map<TAggregateRoot>(dto);
//			await this.Repository.UpdateAsync(item, cancellationToken);
//			this.Mapper.Map(item, dto);
//		}

//		/// <inheritdoc />
//		public virtual async Task UpdateAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
//		{
//			IList<TAggregateRoot> items = this.Mapper.Map<IList<TAggregateRoot>>(dtos);
//			await this.Repository.UpdateAsync(items, cancellationToken);
//			this.Mapper.Map(items, dtos).ToReadOnly();
//		}

//		/// <inheritdoc />
//		public virtual async Task DeleteAsync(TDto dto, CancellationToken cancellationToken = default)
//		{
//			TAggregateRoot item = this.Mapper.Map<TAggregateRoot>(dto);
//			await this.Repository.DeleteAsync(item, cancellationToken);
//			this.Mapper.Map(item, dto);
//		}

//		/// <inheritdoc />
//		public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
//		{
//			await this.Repository.DeleteAsync(id, cancellationToken);
//		}

//		/// <inheritdoc />
//		public virtual async Task DeleteAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
//		{
//			Expression<Func<TAggregateRoot, bool>> mappedPredicate = this.Mapper.MapExpression<Expression<Func<TAggregateRoot, bool>>>(predicate);
//			await this.Repository.DeleteAsync(mappedPredicate, cancellationToken);
//		}

//		/// <inheritdoc />
//		public virtual async Task DeleteAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
//		{
//			IList<TAggregateRoot> items = this.Mapper.Map<IList<TAggregateRoot>>(dtos);
//			await this.Repository.DeleteAsync(items, cancellationToken);
//			this.Mapper.Map(items, dtos);
//		}
//	}
//}


