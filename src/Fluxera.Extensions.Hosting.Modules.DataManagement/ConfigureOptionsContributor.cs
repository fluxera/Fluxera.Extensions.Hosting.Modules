namespace Fluxera.Extensions.Hosting.Modules.DataManagement
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : IConfigureOptionsContributor
	{
		/// <inheritdoc />
		public string Name => "DataManagement";

		/// <inheritdoc />
		public Type OptionsType => typeof(DataManagementOptions);

		/// <inheritdoc />
		public void Configure(IServiceCollection services, IConfigurationSection section)
		{
			services.Configure<DataManagementOptions>(section);
		}
	}
}
