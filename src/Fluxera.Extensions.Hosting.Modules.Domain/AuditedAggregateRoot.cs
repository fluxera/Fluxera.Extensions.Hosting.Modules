namespace Fluxera.Extensions.Hosting.Modules.Domain
{
	using System;
	using Fluxera.ComponentModel.Annotations;
	using Fluxera.Entity;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared;
	using JetBrains.Annotations;

	/// <summary>
	///		A base class for audited aggregate root types.
	/// </summary>
	/// <typeparam name="TAggregateRoot"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public abstract class AuditedAggregateRoot<TAggregateRoot, TKey> : Entity<TAggregateRoot, TKey>, IAuditedObject
		where TAggregateRoot : Entity<TAggregateRoot, TKey>
		where TKey : IComparable<TKey>, IEquatable<TKey>
	{
		/// <inheritdoc />
		[Index]
		public DateTimeOffset? CreatedAt { get; set; }

		/// <inheritdoc />
		[Index]
		public DateTimeOffset? LastModifiedAt { get; set; }

		/// <inheritdoc />
		[Index]
		public DateTimeOffset? DeletedAt { get; set; }

		/// <inheritdoc />
		[Index]
		[Reference("User")]
		public string CreatedBy { get; set; }

		/// <inheritdoc />
		[Index]
		[Reference("User")]
		public string LastModifiedBy { get; set; }

		/// <inheritdoc />
		[Index]
		[Reference("User")]
		public string DeletedBy { get; set; }
	}
}
