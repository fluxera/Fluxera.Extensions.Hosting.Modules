namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Sequence
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for s service that can increment a named sequence.
	/// </summary>
	[PublicAPI]
	public interface ISequenceService
	{
		/// <summary>
		///     Increments the specified sequence.
		/// </summary>
		/// <param name="name">The name of the sequence.</param>
		/// <returns>The current value of the sequence.</returns>
		Task<long> Increment(string name);

		/// <summary>
		///     Resets the specified sequence.
		/// </summary>
		/// <param name="name">The name.</param>
		Task Reset(string name);
	}
}
