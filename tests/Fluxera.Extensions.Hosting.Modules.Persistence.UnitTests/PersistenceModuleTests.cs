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
			IDatabaseNameProviderAdapter databaseNameProviderAdapter = this.ApplicationLoader.ServiceProvider.GetRequiredService<IDatabaseNameProviderAdapter>();
			databaseNameProviderAdapter.Should().NotBeNull();
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
			repositoryOptions.Settings.Should().ContainKey("TestSetting");
			repositoryOptions.Settings.Should().ContainValue("TestValue");
			repositoryOptions.Settings["TestSetting"].Should().Be("TestValue");
		}

		[Test]
		public void ShouldGetDatabaseNameFromProviderAdapter()
		{
			IDatabaseNameProviderAdapter databaseNameProviderAdapter = this.ApplicationLoader.ServiceProvider.GetRequiredService<IDatabaseNameProviderAdapter>();
			string databaseName = databaseNameProviderAdapter.GetDatabaseName((RepositoryName)"Test");
			databaseName.Should().NotBeNullOrWhiteSpace();
			databaseName.Should().Be("prefix-database");
		}
	}
}
