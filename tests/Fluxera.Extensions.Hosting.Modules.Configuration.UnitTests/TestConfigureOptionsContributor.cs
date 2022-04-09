namespace Fluxera.Extensions.Hosting.Modules.Configuration.UnitTests
{
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class TestConfigureOptionsContributor : IConfigureOptionsContributor
	{
		/// <inheritdoc />
		public string Name => "Test";

		/// <inheritdoc />
		public void Configure(IServiceConfigurationContext context, IConfigurationSection section)
		{
			context.Services.Configure<TestOptions>(section);
		}
	}
}
