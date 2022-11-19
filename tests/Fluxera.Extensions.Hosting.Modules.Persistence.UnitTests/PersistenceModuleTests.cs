namespace Fluxera.Extensions.Hosting.Modules.Persistence.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Fluxera.Repository;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class PersistenceModuleTests : StartupModuleTestBase<TestModule>
	{
		[SetUp]
		public void SetUp()
		{
			this.StartApplication();
		}

		[TearDown]
		public void TearDown()
		{
			this.StopApplication();
		}

		[Test]
		public void ShouldAddDatabaseNameProviderAdapter()
		{
			IDatabaseNameProvider databaseNameProvider = this.ApplicationLoader.ServiceProvider.GetRequiredService<IDatabaseNameProvider>();
			databaseNameProvider.Should().NotBeNull();
		}

		[Test]
		public void ShouldConfigurePersistenceOptions()
		{
			IOptions<PersistenceOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<PersistenceOptions>>();
			options.Value.Repositories.Should().NotBeNullOrEmpty();
			options.Value.ConnectionStrings.Should().NotBeNullOrEmpty();

			RepositoryOptions repositoryOptions = options.Value.Repositories.GetOptionsOrDefault("Test");
			repositoryOptions.ProviderName.Should().Be("InMemory");
			repositoryOptions.DatabaseName.Should().Be("database");
			repositoryOptions.DatabaseNamePrefix.Should().Be("prefix");
			repositoryOptions.ConnectionStringName.Should().Be("Default");
			options.Value.ConnectionStrings[repositoryOptions.ConnectionStringName].Should().Be("localhost");
		}

		[Test]
		public void ShouldGetDatabaseNameFromProviderAdapter()
		{
			IDatabaseNameProvider databaseNameProvider = this.ApplicationLoader.ServiceProvider.GetRequiredService<IDatabaseNameProvider>();
			string databaseName = databaseNameProvider.GetDatabaseName((RepositoryName)"Test");
			databaseName.Should().NotBeNullOrWhiteSpace();
			databaseName.Should().Be("prefix-database");
		}
	}
}
