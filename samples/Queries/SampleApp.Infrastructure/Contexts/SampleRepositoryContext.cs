namespace SampleApp.Infrastructure.Contexts
{
	using Fluxera.Repository.MongoDB;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class SampleRepositoryContext : MongoContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(MongoContextOptions options)
		{
			options.UseDbContext<SampleMongoContext>();
		}
	}
}
