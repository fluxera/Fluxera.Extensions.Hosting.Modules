namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
{
	using System;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <summary>
	///     The contract exposes only "Get" methods as an application service.
	/// </summary>
	/// <typeparam name="TDto">The DTO type.</typeparam>
	[PublicAPI]
	public interface ICanGetApplicationService<TDto> : IApplicationService
		where TDto : class, IEntityDto
	{
		/// <summary>
		///     Gets the item identified by the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TDto> GetAsync(string id, CancellationToken cancellationToken = default);

		/// <summary>
		///     Gets the selected result value of the item identified by the given id.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="id"></param>
		/// <param name="selector"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TResult> GetAsync<TResult>(string id, Expression<Func<TDto, TResult>> selector, CancellationToken cancellationToken = default);

		/// <summary>
		///     Checks, if a item identified by the given id exists.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
	}
}
