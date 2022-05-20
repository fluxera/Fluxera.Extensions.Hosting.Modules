namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
	using JetBrains.Annotations;

	/// <summary>
	///     This class can be inherited by DTO classes to implement <see cref="IAuditedObject" /> interface.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public abstract class AuditedEntityDto : EntityDto, IAuditedObject
	{
		/// <inheritdoc />
		public DateTimeOffset? CreatedAt { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? LastModifiedAt { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? DeletedAt { get; set; }

		/// <inheritdoc />
		public string CreatedBy { get; set; }

		/// <inheritdoc />
		public string LastModifiedBy { get; set; }

		/// <inheritdoc />
		public string DeletedBy { get; set; }
	}
}
