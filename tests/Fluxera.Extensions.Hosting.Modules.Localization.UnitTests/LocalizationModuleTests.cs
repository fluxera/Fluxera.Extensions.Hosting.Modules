namespace Fluxera.Extensions.Hosting.Modules.Localization.UnitTests
{
	using System.Linq;
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
		public void ShouldConfigureResourcesPath()
		{
			IModuleDescriptor descriptor = this.ApplicationLoader.Modules.Last();
			LocalizationModule? module = descriptor.Instance as LocalizationModule;

			IOptions<LocalizationOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<LocalizationOptions>>();
			options.Value.ResourcesPath.Should().Be("Localizations");
		}
	}
}
