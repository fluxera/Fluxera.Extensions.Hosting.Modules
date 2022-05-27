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
	///     The contract exposes only "Delete" methods as an application service.
	/// </summary>
	/// <typeparam name="TDto">The DTO type.</typeparam>
	[PublicAPI]
	public interface ICanRemoveApplicationService<TDto> : IApplicationService
		where TDto : class, IEntityDto
	{
		/// <summary>
		///     Remove the given item.
		/// </summary>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task RemoveAsync(TDto dto, CancellationToken cancellationToken = default);

		/// <summary>
		///     Remove the item identifier by the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task RemoveAsync(string id, CancellationToken cancellationToken = default);

		/// <summary>
		///     Remove all items that satisfy the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task RemoveRangeAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default);

		/// <summary>
		///     Remove the given items.
		/// </summary>
		/// <param name="dtos"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task RemoveRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default);
	}
}
