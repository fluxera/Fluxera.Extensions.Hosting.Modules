namespace Fluxera.Extensions.Hosting.Modules.Application.Dtos
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
	using JetBrains.Annotations;

	/// <summary>
	///     This class can be inherited by DTO classes to implement <see cref="IMultiTenancyObject" /> interface.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public abstract class MultiTenancyEntityDto : EntityDto, IMultiTenancyObject
	{
		/// <inheritdoc />
		public string TenantID { get; set; }
	}
}
