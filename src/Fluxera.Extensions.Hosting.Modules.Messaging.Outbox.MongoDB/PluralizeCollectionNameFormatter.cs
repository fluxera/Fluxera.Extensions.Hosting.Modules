namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.MongoDB
{
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using MassTransit;
	using MassTransit.MongoDbIntegration;

	[UsedImplicitly]
	internal sealed class PluralizeCollectionNameFormatter : ICollectionNameFormatter
	{
		/// <inheritdoc />
		public string Saga<TSaga>() where TSaga : ISaga
		{
			return typeof(TSaga).Name.Pluralize();
		}

		/// <inheritdoc />
		public string Collection<T>() where T : class
		{
			return typeof(T).Name.Pluralize();
		}
	}
}
