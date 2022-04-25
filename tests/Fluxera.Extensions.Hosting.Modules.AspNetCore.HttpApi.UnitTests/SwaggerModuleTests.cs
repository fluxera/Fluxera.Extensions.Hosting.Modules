namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Swagger;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class SwaggerModuleTests : StartupModuleTestBase<SwaggerModule>
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
			IOptions<SwaggerOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<SwaggerOptions>>();
			options.Value.Should().NotBeNull();
			options.Value.Enabled.Should().BeTrue();
		}
	}
}
