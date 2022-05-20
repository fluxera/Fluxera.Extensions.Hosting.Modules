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
		public CancellationToken ApplicationStarted => default;

		/// <inheritdoc />
		public CancellationToken ApplicationStopping => default;

		/// <inheritdoc />
		public CancellationToken ApplicationStopped => default;

		/// <inheritdoc />
		public void StopApplication()
		{
		}
	}
}
