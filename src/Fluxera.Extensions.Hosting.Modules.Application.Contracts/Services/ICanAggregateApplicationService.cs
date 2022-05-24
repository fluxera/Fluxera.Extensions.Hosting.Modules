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
		Task<long> CountAsync(CancellationToken cancellationToken = default);

		Task<long> CountAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default);
	}
}
