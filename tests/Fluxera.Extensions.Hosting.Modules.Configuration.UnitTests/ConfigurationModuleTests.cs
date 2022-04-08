namespace Fluxera.Extensions.Hosting.Modules.Configuration.UnitTests
{
	using System;
	using System.Linq;
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
		public void ShouldAddOptionsToServices()
		{
			HostingOptions options = this.ApplicationLoader.Services.GetObject<HostingOptions>();
			options.Should().NotBeNull();
			options.AppName.Should().Be("UnitTests");
			options.Version.Should().Be(Version.Parse("1.0.0"));
			options.Modules.Should().NotBeNullOrEmpty();
			options.Modules.First().Key.Should().Be("Configuration");
			options.Modules.First().Value.Should().NotBeNullOrEmpty();
			options.Modules.First().Value.First().Key.Should().Be("Test");
			options.Modules.First().Value.First().Value.Should().Be("1");
		}

		[Test]
		public void ShouldConfigureOptions()
		{
			IOptions<HostingOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<HostingOptions>>();
			options.Value.Should().NotBeNull();
			options.Value.AppName.Should().Be("UnitTests");
			options.Value.Version.Should().Be(Version.Parse("1.0.0"));
			options.Value.Modules.Should().NotBeNullOrEmpty();
			options.Value.Modules.First().Key.Should().Be("Configuration");
			options.Value.Modules.First().Value.Should().NotBeNullOrEmpty();
			options.Value.Modules.First().Value.First().Key.Should().Be("Test");
			options.Value.Modules.First().Value.First().Value.Should().Be("1");
		}
	}
}
