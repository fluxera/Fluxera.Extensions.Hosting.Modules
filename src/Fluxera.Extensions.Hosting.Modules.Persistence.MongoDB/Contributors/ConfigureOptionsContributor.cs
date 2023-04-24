namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Contributors
{
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<MongoPersistenceOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Persistence";

		/// <inheritdoc />
		protected override void AdditionalConfigure(IServiceConfigurationContext context, MongoPersistenceOptions createdOptions)
		{
			createdOptions.ConnectionStrings = context.Services.GetOptions<ConnectionStrings>();

			context.Log("Configure(MongoPersistenceOptions)",
				services =>
				{
					services.Configure<MongoPersistenceOptions>(options =>
					{
						options.ConnectionStrings = createdOptions.ConnectionStrings;
					});
				});
		}
	}
}
