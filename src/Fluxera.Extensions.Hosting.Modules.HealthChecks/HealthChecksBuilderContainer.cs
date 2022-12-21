namespace Fluxera.Extensions.Hosting.Modules.HealthChecks
{
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     This container class is used to prevent other modules from accessing
	///     the <see cref="IHealthChecksBuilder" /> directly.
	/// </summary>
	internal sealed class HealthChecksBuilderContainer
	{
		public HealthChecksBuilderContainer(IHealthChecksBuilder builder)
		{
			this.Builder = builder;
		}

		public IHealthChecksBuilder Builder { get; }
	}
}
