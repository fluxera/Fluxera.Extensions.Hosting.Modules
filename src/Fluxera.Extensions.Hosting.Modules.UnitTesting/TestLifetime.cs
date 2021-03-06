namespace Fluxera.Extensions.Hosting.Modules.UnitTesting
{
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Hosting;

	/// <summary>
	///     A test host lifetime.
	/// </summary>
	public class TestLifetime : IHostLifetime
	{
		/// <inheritdoc />
		public Task WaitForStartAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
