namespace Fluxera.Extensions.Hosting.Modules.Configuration.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class ConfigurationModuleTests : StartupModuleTestBase<ConfigurationModule>
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
		[Ignore("Need to build a test base that stops after ConfigureServices")]
		public void ShouldAddHostingOptionsToServices()
		{
			HostingOptions options = this.ApplicationLoader.Services.GetObject<HostingOptions>();
			options.Should().NotBeNull();
			options.AppName.Should().Be("UnitTests");
			options.Version.Should().Be(Version.Parse("1.0.0"));
		}

		[Test]
		public void ShouldConfigureHostingOptions()
		{
			IOptions<HostingOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<HostingOptions>>();
			options.Value.Should().NotBeNull();
			options.Value.AppName.Should().Be("UnitTests");
			options.Value.Version.Should().Be(Version.Parse("1.0.0"));
		}
	}
}
