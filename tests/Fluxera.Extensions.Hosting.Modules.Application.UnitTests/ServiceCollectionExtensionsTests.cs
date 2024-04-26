namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests
{
	using System;
	using System.Reflection;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Commands;
	using Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Events;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using global::FluentValidation;
	using MediatR;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class ServiceCollectionExtensionsTests : TestBase
	{
		private IServiceProvider serviceProvider;

		[SetUp]
		public void SetUp()
		{
			this.serviceProvider = BuildServiceProvider(services =>
			{
				services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
				services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			});
		}

		[Test]
		public async Task ShouldFindCommandHandler()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Func<Task> func = async () => await sender.Send(new TestCommand());
			await func.Should().NotThrowAsync();
		}

		[Test]
		public async Task ShouldFindEventHandler()
		{
			IPublisher publisher = this.serviceProvider.GetRequiredService<IPublisher>();

			Func<Task> func = async () => await publisher.Publish(new TestEvent());
			await func.Should().NotThrowAsync();
		}
	}
}
