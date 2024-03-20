namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a dto that is based on an entity.
	/// </summary>
	[PublicAPI]
	public interface IEntityDto<TKey> : IEntityDto
		where TKey : notnull, IComparable<TKey>, IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the ID.
		/// </summary>
		new TKey ID { get; set; }

		//// OData needs this
		//string EntityId { set; }
	}

	/// <summary>
	///     A marker interface for a dto that is based on an entity.
	/// </summary>
	[PublicAPI]
	public interface IEntityDto
	{
		/// <summary>
		///     Gets the ID.
		/// </summary>
		object ID { get; }

		//// OData needs this
		//string EntityId { get; }
	}
}
