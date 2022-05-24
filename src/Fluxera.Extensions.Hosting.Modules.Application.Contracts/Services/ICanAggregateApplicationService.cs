namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
{
	using System;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <summary>
	///     The contract exposes only "Aggregate" methods as an application service.
	/// </summary>
	/// <typeparam name="TDto">The DTO type.</typeparam>
	[PublicAPI]
	public interface ICanAggregateApplicationService<TDto> : IApplicationService
		where TDto : class, IEntityDto
	{
		/// <summary>
		///     Gets the absolute count for the item type.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<long> CountAsync(CancellationToken cancellationToken = default);

		/// <summary>
		///     Gets the count of items that satisfy the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<long> CountAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default);
	}
}
