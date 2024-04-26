namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Threading.Tasks;
	using MadEyeMatt.Results;
	using MadEyeMatt.Results.FluentAssertions;
	using MediatR;
	using System.Reflection;
	using Fluxera.Extensions.Hosting.Modules.Application.Behaviors;
	using Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Commands;
	using Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Events;
	using Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Requests;
	using global::FluentValidation;

	[TestFixture]
	public class ValidationPipelineBehaviorTests : TestBase
	{
		private IServiceProvider serviceProvider;

		[SetUp]
		public void Setup()
		{
			this.serviceProvider = BuildServiceProvider(services =>
			{
				services.AddMediatR(cfg =>
				{
					cfg.MediatorImplementationType = typeof(NotificationValidatingMediator);
					cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				});
				services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
				services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
			});
		}

		[Test]
		public async Task ShouldThrowOnInvalidEvent()
		{
			IPublisher publisher = this.serviceProvider.GetRequiredService<IPublisher>();

			Func<Task> func = async () =>
			{
				await publisher.Publish(new TestEvent());
			};

			await func.Should().ThrowExactlyAsync<ValidationException>();
		}

		[Test]
		public async Task ShouldNotThrowOnInvalidEvent()
		{
			IPublisher publisher = this.serviceProvider.GetRequiredService<IPublisher>();

			await publisher.Publish(new TestEvent
			{
				Name = "Tester"
			});
		}

		[Test]
		public async Task ShouldValidateInvalidCommand()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Result result = await sender.Send(new TestCommand());

			result.Should().BeFailed();
			result.Errors.Should().NotBeNullOrEmpty().And.HaveCount(1);
			result.Errors[0].Message.Should().NotBeNullOrEmpty().And.Be("'Name' must not be empty.");
		}

		[Test]
		public async Task ShouldValidateValidCommand()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Result result = await sender.Send(new TestCommand
			{
				Name = "Tester"
			});

			result.Should().BeSuccessful();
			result.Errors.Should().NotBeNull().And.BeEmpty();
		}

		[Test]
		public async Task ShouldValidateInvalidCommandWithValue()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Result<int> result = await sender.Send(new TestWithValueCommand());

			result.Should().BeFailed();
			result.Errors.Should().NotBeNullOrEmpty().And.HaveCount(1);
			result.Errors[0].Message.Should().NotBeNullOrEmpty().And.Be("'Name' must not be empty.");
		}

		[Test]
		public async Task ShouldValidateValidCommandWithValue()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Result<int> result = await sender.Send(new TestWithValueCommand
			{
				Name = "Tester"
			});

			result.Should().BeSuccessful();
			result.Errors.Should().NotBeNull().And.BeEmpty();
		}

		[Test]
		public async Task ShouldThrowOnInvalidRequestWithoutResult()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Func<Task> func = async () =>
			{
				await sender.Send(new TestRequest());
			};

			await func.Should().ThrowExactlyAsync<ValidationException>();
		}

		[Test]
		public async Task ShouldNotThrowOnValidRequestWithoutResult()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Func<Task> func = async () =>
			{
				await sender.Send(new TestRequest
				{
					Name = "Tester"
				});
			};

			await func.Should().NotThrowAsync();
		}
	}
}
