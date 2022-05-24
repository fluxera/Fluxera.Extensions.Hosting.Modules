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
		Task<TDto> GetAsync(string id, CancellationToken cancellationToken = default);

		Task<TResult> GetAsync<TResult>(string id, Expression<Func<TDto, TResult>> selector,
			CancellationToken cancellationToken = default);

		Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
	}
}
