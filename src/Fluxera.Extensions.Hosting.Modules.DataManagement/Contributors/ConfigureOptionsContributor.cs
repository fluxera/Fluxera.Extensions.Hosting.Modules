namespace Fluxera.Extensions.Hosting.Modules.DataManagement.Contributors
{
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<DataManagementOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "DataManagement";

		/// <inheritdoc />
		protected override void AdditionalConfigure(IServiceConfigurationContext context)
		{
			IConfigurationSection section = context.Configuration.GetSection("ConnectionStrings");
			ConnectionStrings connectionStrings = section.Get<ConnectionStrings>();

			context.Log("Configure(ConnectionStrings)",
				services => services.Configure<ConnectionStrings>(section));

			context.Log("AddObjectAccessor(ConnectionStrings)",
				services => services.AddObjectAccessor(connectionStrings, ObjectAccessorLifetime.ConfigureServices));

			context.Log("Configure(DataManagementOptions)",
				services =>
				{
					services.Configure<DataManagementOptions>(options =>
					{
						options.ConnectionStrings = connectionStrings;
					});
				});
		}
	}
}
