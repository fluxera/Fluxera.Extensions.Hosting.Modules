namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     An entity may provider a dictionary with all non-mapped properties that are
	///     available in the database to support old versions of database entries.
	/// </summary>
	[PublicAPI]
	public interface IExtraPropertiesObject
	{
		/// <summary>
		///     Gets the extra properties.
		/// </summary>
		/// <value>
		///     The extra properties.
		/// </value>
		IDictionary<string, object> ExtraProperties { get; }
	}
}
