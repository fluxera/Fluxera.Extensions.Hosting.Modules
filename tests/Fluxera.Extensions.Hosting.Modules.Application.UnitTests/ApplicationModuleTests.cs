namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Application.Behaviors;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using MediatR;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class ApplicationModuleTests : StartupModuleTestBase<TestModule>
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
		public void ShouldAddDateTimeOffsetProvider()
		{
			IDateTimeOffsetProvider dateTimeOffsetProvider = this.ApplicationLoader.ServiceProvider.GetRequiredService<IDateTimeOffsetProvider>();
			dateTimeOffsetProvider.Should().NotBeNull();
		}

		[Test]
		public void ShouldAddMediator()
		{
			IMediator mediator = this.ApplicationLoader.ServiceProvider.GetRequiredService<IMediator>();
			mediator.Should().NotBeNull();
		}

		[Test]
		public void ShouldAddCustomMediator()
		{
			IMediator mediator = this.ApplicationLoader.ServiceProvider.GetRequiredService<IMediator>();
			mediator.Should().NotBeNull();
			mediator.Should().BeOfType<NotificationValidatingMediator>();
		}
	}
}
