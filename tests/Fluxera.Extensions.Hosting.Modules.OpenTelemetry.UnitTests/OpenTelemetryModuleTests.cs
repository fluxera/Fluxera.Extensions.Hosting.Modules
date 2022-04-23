namespace Fluxera.Extensions.Hosting.Modules.OpenTelemetry.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	public class OpenTelemetryModuleTests : StartupModuleTestBase<OpenTelemetryModule>
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
		public void ShouldConfigureHttpClientOptions()
		{
			IOptions<OpenTelemetryOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<OpenTelemetryOptions>>();
			options.Value.Should().NotBeNull();
			options.Value.OpenTelemetryProtocolEndpoint.Should().NotBeNullOrWhiteSpace();
			options.Value.OpenTelemetryProtocolEndpoint.Should().Be("https://otlp-service:4317");
		}
	}
}
