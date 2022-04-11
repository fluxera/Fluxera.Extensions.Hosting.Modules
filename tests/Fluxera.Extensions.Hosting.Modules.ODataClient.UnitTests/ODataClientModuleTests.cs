namespace Fluxera.Extensions.Hosting.Modules.ODataClient.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Fluxera.Extensions.Http;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class ODataClientModuleTests : StartupModuleTestBase<TestModule>
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
		public void ShouldAddNamedODataClientService()
		{
			ITestService service = this.ApplicationLoader.ServiceProvider.GetService<ITestService>();
			service.Should().NotBeNull();
		}

		[Test]
		public void ShouldConfigureHttpClientOptions()
		{
			IOptions<ODataClientOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<ODataClientOptions>>();
			options.Value.RemoteServices.Should().NotBeNullOrEmpty();
		}

		[Test]
		public void ShouldConfigureRemoteServiceOptions()
		{
			IOptions<RemoteServiceOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<RemoteServiceOptions>>();
			options.Value.Should().NotBeNull();
			options.Value.RemoteServices.Should().NotBeNullOrEmpty();
			options.Value.RemoteServices[RemoteServices.DefaultRemoteServiceName].Should().NotBeNull();
			options.Value.RemoteServices[RemoteServices.DefaultRemoteServiceName].BaseAddress.Should().NotBeNullOrWhiteSpace();
		}
	}
}
