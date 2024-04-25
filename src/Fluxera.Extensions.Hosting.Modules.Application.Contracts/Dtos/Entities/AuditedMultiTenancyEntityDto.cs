namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos.Entities
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared;
	using JetBrains.Annotations;

	/// <summary>
	///     This class can be inherited by DTO classes to implement <see cref="IAuditedObject" /> and
	///     <see cref="IMultiTenancyObject" /> interfaces.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public abstract class AuditedMultiTenancyEntityDto<TKey> : AuditedEntityDto<TKey>, IMultiTenancyObject
		where TKey : notnull, IComparable<TKey>, IEquatable<TKey>
	{
		/// <inheritdoc />
		public TenantId TenantID { get; set; }
	}
}
