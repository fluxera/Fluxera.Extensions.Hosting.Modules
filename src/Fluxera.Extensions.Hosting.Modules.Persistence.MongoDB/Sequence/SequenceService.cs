namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Sequence
{
	using System.Security.Authentication;
	using System.Threading.Tasks;
	using Fluxera.Guards;
	using global::MongoDB.Bson;
	using global::MongoDB.Driver;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class SequenceService : ISequenceService
	{
		private readonly IMongoCollection<BsonDocument> collection;

		public SequenceService(string connectionString, string databaseName)
		{
			Guard.Against.NullOrWhiteSpace(nameof(connectionString), connectionString);
			Guard.Against.NullOrWhiteSpace(nameof(databaseName), databaseName);

			MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));

			bool useSsl = connectionString.Contains("ssl=true");
			if(useSsl)
			{
				settings.SslSettings = new SslSettings
				{
					EnabledSslProtocols = SslProtocols.Tls12,
				};
			}

			this.collection = new MongoClient(settings)
				.GetDatabase(databaseName)
				.GetCollection<BsonDocument>("sequences");
		}

		public async Task<long> Increment(string name)
		{
			FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", name);
			UpdateDefinition<BsonDocument> updateDefinition = Builders<BsonDocument>.Update.Inc("sequence", 1);

			BsonDocument seq = await this.collection.FindOneAndUpdateAsync(filter, updateDefinition);
			if(seq == null)
			{
				BsonDocument bsonDocument = new BsonDocument
				{
					new BsonElement("_id", name),
					new BsonElement("sequence", (long)1),
				};

				await this.collection.InsertOneAsync(bsonDocument);
				seq = await this.collection.FindOneAndUpdateAsync(filter, updateDefinition);
			}

			long sequenceValue = seq["sequence"].AsInt64;
			return sequenceValue;
		}

		public async Task Reset(string name)
		{
			FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", name);
			UpdateDefinition<BsonDocument> updateDefinition = Builders<BsonDocument>.Update.Set("sequence", 1);

			await this.collection.UpdateOneAsync(filter, updateDefinition);
		}
	}
}
