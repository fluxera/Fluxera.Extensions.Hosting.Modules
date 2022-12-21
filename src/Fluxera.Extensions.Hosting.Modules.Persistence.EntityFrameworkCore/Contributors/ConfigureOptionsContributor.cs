namespace Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore.Contributors
{
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<EntityFrameworkCorePersistenceOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Persistence";

		/// <inheritdoc />
		protected override void AdditionalConfigure(IServiceConfigurationContext context, EntityFrameworkCorePersistenceOptions createdOptions)
		{
			createdOptions.ConnectionStrings = context.Services.GetOptions<ConnectionStrings>();

			context.Log("Configure(EntityFrameworkCorePersistenceOptions)",
				services =>
				{
					services.Configure<EntityFrameworkCorePersistenceOptions>(options =>
					{
						options.ConnectionStrings = createdOptions.ConnectionStrings;
					});
				});
		}
	}
}
