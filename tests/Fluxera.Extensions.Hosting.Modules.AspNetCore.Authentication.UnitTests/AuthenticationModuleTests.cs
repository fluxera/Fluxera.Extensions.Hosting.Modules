namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Cookies;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class AuthenticationModuleTests : StartupModuleTestBase<TestModule>
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
		public void ShouldConfigureApiKeyAuthenticationOptions()
		{
			IOptions<ApiKeyAuthenticationOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<ApiKeyAuthenticationOptions>>();
			options.Value.Should().NotBeNull();

			options.Value.Schemes.Should().NotBeNullOrEmpty();
			options.Value.Schemes.Should().HaveCount(2);
		}

		[Test]
		public void ShouldConfigureAuthenticationOptions()
		{
			IOptions<AuthenticationOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<AuthenticationOptions>>();
			options.Value.Should().NotBeNull();
		}

		[Test]
		public void ShouldConfigureBasicAuthenticationOptions()
		{
			IOptions<BasicAuthenticationOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<BasicAuthenticationOptions>>();
			options.Value.Should().NotBeNull();

			options.Value.Schemes.Should().NotBeNullOrEmpty();
			options.Value.Schemes.Should().HaveCount(2);
		}

		[Test]
		public void ShouldConfigureCookiesAuthenticationOptions()
		{
			IOptions<CookiesAuthenticationOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<CookiesAuthenticationOptions>>();
			options.Value.Should().NotBeNull();

			options.Value.Schemes.Should().NotBeNullOrEmpty();
			options.Value.Schemes.Should().HaveCount(2);
		}

		[Test]
		public void ShouldConfigureJwtBearerAuthenticationOptions()
		{
			IOptions<JwtBearerAuthenticationOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<JwtBearerAuthenticationOptions>>();
			options.Value.Should().NotBeNull();

			options.Value.Schemes.Should().NotBeNullOrEmpty();
			options.Value.Schemes.Should().HaveCount(2);
		}
	}
}
