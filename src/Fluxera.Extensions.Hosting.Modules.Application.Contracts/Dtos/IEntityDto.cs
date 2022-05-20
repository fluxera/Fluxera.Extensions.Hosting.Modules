namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a dto that is based on an entity.
	/// </summary>
	[PublicAPI]
	public interface IEntityDto
	{
		/// <summary>
		///     Gets or sets the ID.
		/// </summary>
		string /*TKey*/ ID { get; set; }
	}
}
