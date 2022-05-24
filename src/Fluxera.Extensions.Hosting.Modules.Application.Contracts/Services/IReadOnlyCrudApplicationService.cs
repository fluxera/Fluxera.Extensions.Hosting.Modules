namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <inheritdoc cref="ICanGetApplicationService{TDto}" />
	/// <inheritdoc cref="ICanFindApplicationService{TDto}" />
	/// <inheritdoc cref="ICanAggregateApplicationService{TDto}" />
	/// <summary>
	///     Contract for an application services that reads <typeparamref name="TDto" /> instances.
	/// </summary>
	/// <typeparam name="TDto">The DTO type.</typeparam>
	[PublicAPI]
	public interface IReadOnlyCrudApplicationService<TDto> : ICrudApplicationService,
		ICanGetApplicationService<TDto>,
		ICanFindApplicationService<TDto>,
		ICanAggregateApplicationService<TDto>
		where TDto : class, IEntityDto
	{
	}
}
