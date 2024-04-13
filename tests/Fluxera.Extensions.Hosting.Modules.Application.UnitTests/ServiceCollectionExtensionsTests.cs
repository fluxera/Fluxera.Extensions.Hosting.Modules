namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using MadEyeMatt.Results;
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
				services.AddMediatR();
			});
		}

		[Test]
		public async Task ShouldFindCommandHandler()
		{
			ISender sender = this.serviceProvider.GetService<ISender>();

			Func<Task> func = async () => await sender.Send(new TestRequest());
			await func.Should().NotThrowAsync();
		}

		[Test]
		public async Task ShouldFindEventHandler()
		{
			IPublisher publisher = this.serviceProvider.GetService<IPublisher>();

			Func<Task> func = async () => await publisher.Publish(new TestNotification());
			await func.Should().NotThrowAsync();
		}
	}

	public class TestRequest : IRequest<Result>
	{
	}

	public class TestNotification : INotification
	{
	}

	public class TestRequestHandler : IRequestHandler<TestRequest, Result>
	{
		/// <inheritdoc />
		public Task<Result> Handle(TestRequest request, CancellationToken cancellationToken)
		{
			return Task.FromResult(Result.Ok());
		}
	}

	public class TestNotificationHandler : INotificationHandler<TestNotification>
	{
		/// <inheritdoc />
		public Task Handle(TestNotification notification, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
