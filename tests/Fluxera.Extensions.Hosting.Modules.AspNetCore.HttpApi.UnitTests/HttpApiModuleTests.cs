namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class HttpApiModuleTests : StartupModuleTestBase<TestModule>
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

		/// <inheritdoc />
		protected override void OnStartApplication(IServiceCollection services)
		{
			services.AddTransient<IHttpContextFactory, DefaultHttpContextFactory>();
		}

		[Test]
		public void ShouldConfigureCachingSwaggerOptions()
		{
			IOptions<HttpApiOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<HttpApiOptions>>();
			options.Value.Should().NotBeNull();
		}
	}
}
