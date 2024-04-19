namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared;
	using JetBrains.Annotations;

	/// <summary>
	///     This class can be inherited by DTO classes to implement <see cref="IMultiTenancyObject" /> interface.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public abstract class MultiTenancyEntityDto<TKey> : EntityDto<TKey>, IMultiTenancyObject
		where TKey : notnull, IComparable<TKey>, IEquatable<TKey>
	{
		/// <inheritdoc />
		public TenantId TenantID { get; set; }
	}
}
