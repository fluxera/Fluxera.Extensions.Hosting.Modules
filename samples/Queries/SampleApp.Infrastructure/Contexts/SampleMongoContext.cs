namespace SampleApp.Infrastructure.Contexts
{
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using MadEyeMatt.MongoDB.DbContext;

	[UsedImplicitly]
	internal sealed class SampleMongoContext : MongoDbContext
	{
		/// <inheritdoc />
		protected override void OnConfiguring(MongoDbContextOptionsBuilder builder)
		{
			builder.UseDatabase("mongodb://localhost:27017", "queries-sample");
		}

		/// <inheritdoc />
		public override string GetCollectionName<TDocument>()
		{
			return base.GetCollectionName<TDocument>().Replace("Dto", "").Pluralize();
		}
	}
}
