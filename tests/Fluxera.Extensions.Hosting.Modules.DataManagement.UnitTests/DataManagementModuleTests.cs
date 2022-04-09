namespace Fluxera.Extensions.Hosting.Modules.DataManagement.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class DataManagementModuleTests : StartupModuleTestBase<DataManagementModule>
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
		public void ShouldAddConnectionStringResolver()
		{
			IConnectionStringResolver connectionStringResolver = this.ApplicationLoader.ServiceProvider.GetRequiredService<IConnectionStringResolver>();
			connectionStringResolver.Should().NotBeNull();
			connectionStringResolver.Should().BeOfType<DefaultConnectionStringResolver>();
		}

		[Test]
		public void ShouldConfigureConnectionStrings()
		{
			IOptions<ConnectionStrings> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<ConnectionStrings>>();
			options.Value.Default.Should().Be("localhost");
			options.Value["Database"].Should().Be("server");
		}

		[Test]
		public void ShouldConfigureDataManagement()
		{
			IOptions<DataManagementOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<DataManagementOptions>>();
			options.Value.Should().NotBeNull();
			options.Value.ConnectionStrings.Default.Should().Be("localhost");
			options.Value.ConnectionStrings["Database"].Should().Be("server");
		}
	}
}
