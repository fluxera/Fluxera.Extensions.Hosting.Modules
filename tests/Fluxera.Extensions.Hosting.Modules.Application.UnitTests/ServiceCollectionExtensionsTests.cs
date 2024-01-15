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

			Func<Task> func = async () => await sender.Send(new TestCommand());
			await func.Should().NotThrowAsync();
		}

		[Test]
		public async Task ShouldFindQueryHandler()
		{
			ISender sender = this.serviceProvider.GetService<ISender>();

			Func<Task> func = async () => await sender.Send(new TestQuery());
			await func.Should().NotThrowAsync();
		}

		[Test]
		public async Task ShouldFindEventHandler()
		{
			IPublisher publisher = this.serviceProvider.GetService<IPublisher>();

			Func<Task> func = async () => await publisher.Publish(new TestEvent());
			await func.Should().NotThrowAsync();
		}
	}

	public class TestCommand : ICommand
	{
	}

	public class TestQuery : IQuery<Result<int>>
	{
	}

	public class TestEvent : IEvent
	{
	}

	public class TestCommandHandler : ICommandHandler<TestCommand>
	{
		/// <inheritdoc />
		public Task<Result> Handle(TestCommand request, CancellationToken cancellationToken)
		{
			return Task.FromResult(Result.Ok());
		}
	}

	public class TestQueryHandler : IQueryHandler<TestQuery, Result<int>>
	{
		/// <inheritdoc />
		public Task<Result<int>> Handle(TestQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult(Result<int>.Ok(1));
		}
	}

	public class TestEventHandler : IEventHandler<TestEvent>
	{
		/// <inheritdoc />
		public Task Handle(TestEvent notification, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
