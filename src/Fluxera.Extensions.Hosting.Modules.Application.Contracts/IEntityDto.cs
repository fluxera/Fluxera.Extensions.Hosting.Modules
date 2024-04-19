namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using System;

	/* Unmerged change from project 'Fluxera.Extensions.Hosting.Modules.Application.Contracts (net6.0)'
	Before:
		using JetBrains.Annotations;
	After:
		using Fluxera;
		using Fluxera.Extensions;
		using Fluxera.Extensions.Hosting;
		using Fluxera.Extensions.Hosting.Modules;
		using Fluxera.Extensions.Hosting.Modules.Application;
		using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
		using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
		using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
		using JetBrains.Annotations;
	*/
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
	}
}
