namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Cors.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	public class CorsModuleTests : StartupModuleTestBase<CorsModule>
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
		public void ShouldConfigureCorsOptions()
		{
			IOptions<CorsOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<CorsOptions>>();
			options.Value.Should().NotBeNull();

			options.Value.Origins.Should().NotBeNullOrEmpty();
			options.Value.Origins.Should().HaveCount(2);
			options.Value.Origins[0].Should().Be("http://localhost:5000");
			options.Value.Origins[1].Should().Be("https://localhost:5001");
		}
	}
}
