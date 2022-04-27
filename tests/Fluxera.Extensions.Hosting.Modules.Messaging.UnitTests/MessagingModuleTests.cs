namespace Fluxera.Extensions.Hosting.Modules.Messaging.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class MessagingModuleTests : StartupModuleTestBase<TestModule>
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
		public void ShouldConfigureMessagingOptions()
		{
			IOptions<MessagingOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<MessagingOptions>>();
			options.Value.Should().NotBeNull();
		}
	}
}
