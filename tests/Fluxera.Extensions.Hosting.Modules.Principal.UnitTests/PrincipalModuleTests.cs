namespace Fluxera.Extensions.Hosting.Modules.Principal.UnitTests
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Security.Principal;
	using System.Threading;
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.Principal.Extensions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class PrincipalModuleTests : StartupModuleTestBase<PrincipalModule>
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
		public void ShouldAddPrincipalAccessor()
		{
			IPrincipalAccessor principalAccessor = this.ApplicationLoader.ServiceProvider.GetRequiredService<IPrincipalAccessor>();
			principalAccessor.Should().NotBeNull();
		}

		[Test]
		public void ShouldAddPrincipalFactory()
		{
			Thread.CurrentPrincipal = new ClaimsPrincipal();
			IPrincipal principal = this.ApplicationLoader.ServiceProvider.GetRequiredService<IPrincipal>();
			principal.Should().NotBeNull();
		}

		[Test]
		public void ShouldUseThreadBasedClaimsPrincipalProvider()
		{
			Thread.CurrentPrincipal = new ClaimsPrincipal();
			ClaimsPrincipal principal = this.ApplicationLoader.ServiceProvider.GetRequiredService<ClaimsPrincipal>();
			principal.Should().NotBeNull();
		}

		[Test]
		public void ShouldUseThreadBasedPrincipalProvider()
		{
			IList<IPrincipalProvider> principalProviders = this.ApplicationLoader.ServiceProvider.GetServices<IPrincipalProvider>().ToList();
			principalProviders.Should().NotBeNull();
			principalProviders.Count.Should().Be(1);
			principalProviders.First().Should().BeOfType<ThreadPrincipalProvider>();
			principalProviders.First().Position.Should().Be(int.MaxValue);
		}
	}
}
