namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc.Infrastructure;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class AspNetCoreModuleTests : StartupModuleTestBase<AspNetCoreModule>
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
		public void ShouldAddActionContextAccessor()
		{
			IActionContextAccessor actionContextAccessor = this.ApplicationLoader.ServiceProvider.GetRequiredService<IActionContextAccessor>();
			actionContextAccessor.Should().NotBeNull();
		}

		[Test]
		public void ShouldAddHttpContextAccessor()
		{
			IHttpContextAccessor httpContextAccessor = this.ApplicationLoader.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
			httpContextAccessor.Should().NotBeNull();
		}

		[Test]
		public void ShouldConfigureAspNetCoreOptions()
		{
			IOptions<AspNetCoreOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<AspNetCoreOptions>>();
			options.Value.Should().NotBeNull();
			options.Value.BaseUrl.Should().NotBeNull();
			options.Value.BaseUrl.Should().Be(new Uri("https://localhost:5001/"));
			options.Value.BaseUrl.Should().Be(new Uri("https://localhost:5001"));
		}
	}
}
