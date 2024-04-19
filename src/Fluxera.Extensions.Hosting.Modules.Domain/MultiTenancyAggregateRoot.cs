namespace Fluxera.Extensions.Hosting.Modules.Domain
{
	using System;
	using Fluxera.ComponentModel.Annotations;
	using Fluxera.Entity;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared;
	using JetBrains.Annotations;

	/// <summary>
	///		A base class for single database multi-tenancy aggregate root types.
	/// </summary>
	/// <typeparam name="TAggregateRoot"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public abstract class MultiTenancyAggregateRoot<TAggregateRoot, TKey> : AggregateRoot<TAggregateRoot, TKey>, IMultiTenancyObject
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
		where TKey : IComparable<TKey>, IEquatable<TKey>
	{
		/// <inheritdoc />
		[Index]
		[Reference("Tenant")]
		public TenantId TenantID { get; set; }
	}
}
