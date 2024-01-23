namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for an object that can be used in a patch operation.
	/// </summary>
	[PublicAPI]
	public interface IPatchableObject
	{
		/// <summary>
		///     Gets the change tracker.
		/// </summary>
		/// <value>
		///     The change tracker.
		/// </value>
		IChangeTracker ChangeTracker { get; }
	}
}
