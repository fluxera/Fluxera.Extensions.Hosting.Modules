namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Threading.Tasks;
	using FluentValidation;
	using Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models;
	using MadEyeMatt.Results;
	using MadEyeMatt.Results.FluentAssertions;
	using MediatR;

	[TestFixture]
	public class ValidationPipelineBehaviorTests : TestBase
	{
		private IServiceProvider serviceProvider;

		[SetUp]
		public void Setup()
		{
			this.serviceProvider = BuildServiceProvider(services =>
			{
				services.AddMediatR();
			});
		}

		[Test]
		public async Task ShouldValidateInvalidRequest()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Result result = await sender.Send(new TestRequest());

			result.Should().BeFailed();
			result.Errors.Should().NotBeNullOrEmpty().And.HaveCount(1);
			result.Errors[0].Message.Should().NotBeNullOrEmpty().And.Be("'Name' must not be empty.");
		}

		[Test]
		public async Task ShouldValidateValidRequest()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Result result = await sender.Send(new TestRequest
			{
				Name = "Tester"
			});

			result.Should().BeSuccessful();
			result.Errors.Should().NotBeNull().And.BeEmpty();
		}

		[Test]
		public async Task ShouldValidateInvalidRequestWithValue()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Result<int> result = await sender.Send(new TestWithValueRequest());

			result.Should().BeFailed();
			result.Errors.Should().NotBeNullOrEmpty().And.HaveCount(1);
			result.Errors[0].Message.Should().NotBeNullOrEmpty().And.Be("'Name' must not be empty.");
		}

		[Test]
		public async Task ShouldValidateValidRequestWithValue()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Result<int> result = await sender.Send(new TestWithValueRequest
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
				await sender.Send(new TestWithoutResultRequest());
			};

			await func.Should().ThrowExactlyAsync<ValidationException>();
		}

		[Test]
		public async Task ShouldNotThrowOnValidRequestWithoutResult()
		{
			ISender sender = this.serviceProvider.GetRequiredService<ISender>();

			Func<Task> func = async () =>
			{
				await sender.Send(new TestWithoutResultRequest
				{
					Name = "Tester"
				});
			};

			await func.Should().NotThrowAsync();
		}
	}
}
