namespace Fluxera.Extensions.Hosting.Modules.DataManagement.Contributors
{
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : IConfigureOptionsContributor
	{
		/// <inheritdoc />
		public string Name => "DataManagement";

		/// <inheritdoc />
		public void Configure(IServiceConfigurationContext context, IConfigurationSection section)
		{
			IConfigurationSection connectionStringsSection = context.Configuration.GetSection("ConnectionStrings");
			ConnectionStrings connectionStrings = connectionStringsSection.Get<ConnectionStrings>();

			context.Log("AddObjectAccessor(ConnectionStrings)",
				services => services.AddObjectAccessor(connectionStrings, ObjectAccessorLifetime.ConfigureServices));

			context.Services.Configure<ConnectionStrings>(connectionStringsSection);
			context.Services.Configure<DataManagementOptions>(section);
			context.Services.Configure<DataManagementOptions>(options =>
			{
				options.ConnectionStrings = connectionStrings;
			});
		}
	}
}
