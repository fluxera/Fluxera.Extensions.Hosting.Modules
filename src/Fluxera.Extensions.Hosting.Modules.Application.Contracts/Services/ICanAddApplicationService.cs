namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
{
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <summary>
	///     The contract exposes only "Add" methods as an application service.
	/// </summary>
	/// <typeparam name="TDto">The DTO type.</typeparam>
	[PublicAPI]
	public interface ICanAddApplicationService<in TDto> : IApplicationService
		where TDto : class, IEntityDto
	{
		/// <summary>
		///     Adds the given item.
		/// </summary>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task AddAsync(TDto dto, CancellationToken cancellationToken = default);

		/// <summary>
		///     Adds teh given items.
		/// </summary>
		/// <param name="dtos"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task AddRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default);
	}
}
