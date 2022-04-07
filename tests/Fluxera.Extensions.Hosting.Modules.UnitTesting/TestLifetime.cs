namespace Fluxera.Extensions.Hosting.Modules.UnitTesting
{
	using Microsoft.Extensions.Hosting;

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
