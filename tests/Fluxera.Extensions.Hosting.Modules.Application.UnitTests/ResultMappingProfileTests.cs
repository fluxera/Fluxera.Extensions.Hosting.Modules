﻿namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Extensions.Hosting.Modules.Application.Contributors;
	using global::AutoMapper;
	using MadEyeMatt.Results;
	using NUnit.Framework;

	[TestFixture]
	public class ResultMappingProfileTests
	{
		private IMapper mapper;
		private MapperConfiguration config;

		[SetUp]
		public void Setup()
		{
			this.config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<ResultMappingProfile>();
				cfg.AddProfile<TestMappingProfile>();
			});
			this.mapper = this.config.CreateMapper();
		}

		[Test]
		public void ShouldHaveValidMappingConfiguration()
		{
			this.config.AssertConfigurationIsValid();
		}

		[Test]
		public void ShouldMapSuccessfulResult_SimpleValue()
		{
			Result<string> result = Result<string>.Ok("Hallo!");
			ResultDto<string> resultDto = this.mapper.Map<ResultDto<string>>(result);

			resultDto.Should().NotBeNull();
			resultDto.IsSuccessful.Should().BeTrue();
			resultDto.IsFailed.Should().BeFalse();
			resultDto.Successes.Should().BeNullOrEmpty();
			resultDto.Errors.Should().BeNullOrEmpty();
			resultDto.Value.Should().Be("Hallo!");
		}

		[Test]
		public void ShouldMapFailedResult_SimpleValue()
		{
			Result<string> result = Result<string>.Fail("An error occurred.");
			ResultDto<string> resultDto = this.mapper.Map<ResultDto<string>>(result);

			resultDto.Should().NotBeNull();
			resultDto.IsSuccessful.Should().BeFalse();
			resultDto.IsFailed.Should().BeTrue();
			resultDto.Successes.Should().BeNullOrEmpty();
			resultDto.Errors.Should().NotBeNullOrEmpty().And.HaveCount(1);
			resultDto.Errors[0].Message.Should().Be("An error occurred.");
			resultDto.Value.Should().BeNull();
		}

		[Test]
		public void ShouldMapSuccessfulResult_ComplexValue()
		{
			Result<ComplexType> result = Result<ComplexType>.Ok(new ComplexType { Message = "Hallo!", Amount = 42 });
			ResultDto<ComplexType> resultDto = this.mapper.Map<ResultDto<ComplexType>>(result);

			resultDto.Should().NotBeNull();
			resultDto.IsSuccessful.Should().BeTrue();
			resultDto.IsFailed.Should().BeFalse();
			resultDto.Successes.Should().BeNullOrEmpty();
			resultDto.Errors.Should().BeNullOrEmpty();
			resultDto.Value.Should().NotBeNull();
			resultDto.Value.Message.Should().Be("Hallo!");
			resultDto.Value.Amount.Should().Be(42);
		}

		[Test]
		public void ShouldMapFailedResult_ComplexValue()
		{
			Result<ComplexType> result = Result<ComplexType>.Fail("An error occurred.");
			ResultDto<ComplexType> resultDto = this.mapper.Map<ResultDto<ComplexType>>(result);

			resultDto.Should().NotBeNull();
			resultDto.IsSuccessful.Should().BeFalse();
			resultDto.IsFailed.Should().BeTrue();
			resultDto.Successes.Should().BeNullOrEmpty();
			resultDto.Errors.Should().NotBeNullOrEmpty().And.HaveCount(1);
			resultDto.Errors[0].Message.Should().Be("An error occurred.");
			resultDto.Value.Should().BeNull();
		}
	}

	public class ComplexType
	{
		public string Message { get; set; }

		public int Amount { get; set; }
	}

	public class ComplexTypeDto
	{
		public string Message { get; set; }

		public int Amount { get; set; }
	}

	public class TestMappingProfile : Profile
	{
		public TestMappingProfile()
		{
			this.CreateMap<ComplexType, ComplexTypeDto>();
		}
	}
}