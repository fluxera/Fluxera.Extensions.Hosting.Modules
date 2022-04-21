namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class AuthenticationModuleTests : StartupModuleTestBase<AuthenticationModule>
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
		public void ShouldConfigureCachingRedisOptions()
		{
			IOptions<AuthenticationOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<AuthenticationOptions>>();
			options.Value.Should().NotBeNull();
		}
	}
}
