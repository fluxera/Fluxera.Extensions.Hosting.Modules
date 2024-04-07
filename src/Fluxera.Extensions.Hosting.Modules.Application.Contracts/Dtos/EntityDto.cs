namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
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

		/// <inheritdoc cref="IEntityDto" />
		/// <inheritdoc cref="IEntityDto{TKey}" />
		public string EntityId
		{
			get => this.ID.ToString();
			set => this.ID = this.CreateKey(value);
		}

		/// <inheritdoc />
		object IEntityDto.ID => this.ID;

		/// <summary>
		///		Create an instance of the strongly-typed ID.
		/// </summary>
		/// <param name="entityId"></param>
		/// <returns></returns>
		protected abstract TKey CreateKey(string entityId);

		/// <inheritdoc />
		public override string ToString()
		{
			return $"DTO: {this.GetType().Name}, ID = {this.ID}";
		}
	}
}
