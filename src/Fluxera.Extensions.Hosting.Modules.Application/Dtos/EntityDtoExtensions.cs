namespace Fluxera.Extensions.Hosting.Modules.Application.Dtos
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <summary>
	///     Extensions for the <see cref="IEntityDto" /> type.
	/// </summary>
	[PublicAPI]
	public static class EntityDtoExtensions
	{
		/// <summary>
		///     Determines whether this instance is transient.
		/// </summary>
		/// <typeparam name="TDto">The type of the dto.</typeparam>
		/// <param name="dto">The entity.</param>
		/// <returns>
		///     <c>true</c> if the specified entity is transient; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsTransient<TDto>(this TDto dto) where TDto : class, IEntityDto
		{
			return string.IsNullOrWhiteSpace(dto.ID);
		}
	}
}
