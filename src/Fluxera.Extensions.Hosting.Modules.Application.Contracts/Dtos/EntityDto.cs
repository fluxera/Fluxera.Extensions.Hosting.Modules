namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for dtos that represent an entity.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public abstract class EntityDto : IEntityDto
	{
		/// <summary>
		///     The unique ID of the entity.
		/// </summary>
		public string ID { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return $"DTO: {this.GetType().Name}, ID = {this.ID}";
		}
	}
}
