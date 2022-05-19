namespace Fluxera.Extensions.Hosting.Modules.UnitTesting
{
	using System.Threading;
	using Microsoft.Extensions.Hosting;

	/// <summary>
	///     A test host lifetime.
	/// </summary>
	public class TestApplicationLifetime : IHostApplicationLifetime
	{
		/// <inheritdoc />
		public CancellationToken ApplicationStarted { get; }

		/// <inheritdoc />
		public CancellationToken ApplicationStopping { get; }

		/// <inheritdoc />
		public CancellationToken ApplicationStopped { get; }

		/// <inheritdoc />
		public void StopApplication()
		{
		}
	}
}
