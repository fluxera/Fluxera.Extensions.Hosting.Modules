namespace Fluxera.Extensions.Hosting.Modules.Localization.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Localization;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class LocalizationModuleTests : StartupModuleTestBase<LocalizationModule>
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
		public void ShouldAddLocalizerService()
		{
			IStringLocalizerFactory stringLocalizerFactory = this.ApplicationLoader.ServiceProvider.GetRequiredService<IStringLocalizerFactory>();
			stringLocalizerFactory.Should().NotBeNull();
		}

		[Test]
		public void ShouldConfigureResourcesPath()
		{
			IOptions<LocalizationOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<LocalizationOptions>>();
			options.Value.ResourcesPath.Should().Be("Localizations");
		}
	}
}
