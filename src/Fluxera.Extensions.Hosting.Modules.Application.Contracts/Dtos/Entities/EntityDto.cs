namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos.Entities
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for DTOs that represent an entity.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public abstract class EntityDto<TKey> : IEntityDto<TKey>
		where TKey : notnull, IComparable<TKey>, IEquatable<TKey>
	{
		/// <summary>
		///     The unique ID of the entity.
		/// </summary>
		public TKey ID { get; set; }

		/// <inheritdoc />
		object IEntityDto.ID => this.ID;

		/// <inheritdoc />
		public override string ToString()
		{
			return $"DTO: {this.GetType().Name}, ID = {this.ID}";
		}
	}
}
