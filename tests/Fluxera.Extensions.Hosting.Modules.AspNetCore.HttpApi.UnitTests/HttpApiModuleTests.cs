namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class HttpApiModuleTests : StartupModuleTestBase<TestModule>
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
		public void ShouldConfigureCachingSwaggerOptions()
		{
			IOptions<HttpApiOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<HttpApiOptions>>();
			options.Value.Should().NotBeNull();
		}
	}
}
