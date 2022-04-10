namespace Fluxera.Extensions.Hosting.Modules.Configuration.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class ModuleConfigureOptionsTests : StartupModuleTestBase<TestModule>
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
		public void ShouldConfigureTestOptions()
		{
			IOptions<TestOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<TestOptions>>();
			options.Value.Integer.Should().Be(1);
			options.Value.String.Should().Be("Hello");
		}
	}
}
