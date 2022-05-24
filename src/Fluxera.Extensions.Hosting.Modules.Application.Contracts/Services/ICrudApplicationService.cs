namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <inheritdoc cref="ICanAddApplicationService{TDto}" />
	/// <inheritdoc cref="ICanUpdateApplicationService{TDto}" />
	/// <inheritdoc cref="ICanRemoveApplicationService{TDto}" />
	/// <inheritdoc cref="IReadOnlyCrudApplicationService{TDto}" />
	/// <summary>
	///     Contract for an application services that reads and writes <typeparamref name="TDto" /> instances.
	/// </summary>
	/// <typeparam name="TDto">The DTO type.</typeparam>
	[PublicAPI]
	public interface ICrudApplicationService<TDto> : ICrudApplicationService,
		ICanAddApplicationService<TDto>,
		ICanUpdateApplicationService<TDto>,
		ICanRemoveApplicationService<TDto>,
		IReadOnlyCrudApplicationService<TDto>
		where TDto : class, IEntityDto
	{
	}

	/// <summary>
	///     Marker interface for a special CRUD application service.
	/// </summary>
	[PublicAPI]
	public interface ICrudApplicationService : IApplicationService
	{
	}
}
