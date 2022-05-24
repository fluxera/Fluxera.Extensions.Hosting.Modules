namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <summary>
	///     The contract exposes only "Find" methods as an application service.
	/// </summary>
	/// <typeparam name="TDto">The DTO type.</typeparam>
	[PublicAPI]
	public interface ICanFindApplicationService<TDto> : IApplicationService
		where TDto : class, IEntityDto
	{
		Task<TDto> FindOneAsync(Expression<Func<TDto, bool>> predicate, /* IQueryOptions<TDto> queryOptions = null,*/
			CancellationToken cancellationToken = default);

		Task<TResult> FindOneAsync<TResult>(Expression<Func<TDto, bool>> predicate,
			Expression<Func<TDto, TResult>> selector, /* IQueryOptions<TDto> queryOptions = null,*/
			CancellationToken cancellationToken = default);

		Task<bool> ExistsAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default);

		Task<IReadOnlyList<TDto>> FindManyAsync(Expression<Func<TDto, bool>> predicate,
			/*IQueryOptions<TDto> queryOptions = null, */CancellationToken cancellationToken = default);

		Task<IReadOnlyList<TResult>> FindManyAsync<TResult>(Expression<Func<TDto, bool>> predicate,
			Expression<Func<TDto, TResult>> selector, /* IQueryOptions<TDto> queryOptions = null,*/
			CancellationToken cancellationToken = default);
	}
}
