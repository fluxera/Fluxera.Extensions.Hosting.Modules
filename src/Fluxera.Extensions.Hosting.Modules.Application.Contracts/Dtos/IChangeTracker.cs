namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System.Runtime.CompilerServices;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for an object that tracks changes.
	/// </summary>
	[PublicAPI]
	public interface IChangeTracker
	{
		/// <summary>
		///     Gets the count.
		/// </summary>
		/// <value>
		///     The count.
		/// </value>
		int Count { get; }

		/// <summary>
		///     Gets an anonymous object containing the changes.
		/// </summary>
		/// <returns></returns>
		object GetChangesObject();

		/// <summary>
		///     Gets the specified property name.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns></returns>
		TValue Get<TValue>([CallerMemberName] string propertyName = null);

		/// <summary>
		///     Sets if changed.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="newValue">The new value.</param>
		/// <param name="propertyName">Name of the property.</param>
		void SetIfChanged<TValue>(TValue newValue, [CallerMemberName] string propertyName = null);
	}
}
