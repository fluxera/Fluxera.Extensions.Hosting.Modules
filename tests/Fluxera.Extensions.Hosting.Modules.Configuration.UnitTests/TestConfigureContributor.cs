namespace Fluxera.Extensions.Hosting.Modules.Configuration.UnitTests
{
	using System;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class TestConfigureContributor : IConfigureContributor
	{
		/// <inheritdoc />
		public string Name => "Test";

		/// <inheritdoc />
		public Type OptionsType => typeof(TestOptions);

		/// <inheritdoc />
		public void Configure(IServiceCollection services, IConfigurationSection section)
		{
			services.Configure<TestOptions>(section);
		}
	}
}
