namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
	using JetBrains.Annotations;

	/// <summary>
	///     This class can be inherited by DTO classes to implement <see cref="IAuditedObject" /> and
	///     <see cref="IMultiTenancyObject" /> interfaces.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public abstract class AuditedMultiTenancyEntityDto : AuditedEntityDto, IMultiTenancyObject
	{
		/// <inheritdoc />
		public string TenantID { get; set; }
	}
}
