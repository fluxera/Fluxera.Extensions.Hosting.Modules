namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor.UnitTests
{
	using System.Linq;
	using System.Reflection;
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class BlazorModuleTests : StartupModuleTestBase<TestModule>
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
		public void ShouldProvideAssemblies()
		{
			IBlazorAssembliesProvider provider = this.ApplicationLoader.ServiceProvider.GetRequiredService<IBlazorAssembliesProvider>();
			provider.Should().NotBeNull();

			provider.AdditionalAssemblies.Should().NotBeNullOrEmpty();
			provider.AdditionalAssemblies.Should().HaveCount(1);

			Assembly assembly = provider.AdditionalAssemblies.First();
			assembly.Should().NotBeNull();
			(assembly == Assembly.GetExecutingAssembly()).Should().BeTrue();
		}
	}
}
