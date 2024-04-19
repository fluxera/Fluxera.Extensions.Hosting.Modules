namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     This interface is defined to standardize to return a list of items to clients.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[PublicAPI]
	public interface IListResult<out T>
	{
		/// <summary>
		///     List of items.
		/// </summary>
		IReadOnlyList<T> Items { get; }
	}
}
