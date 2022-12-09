//namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests
//{
//	using System.Linq;
//	using FluentAssertions;
//	using Fluxera.Entity;
//	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
//	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Query;
//	using Fluxera.Extensions.Hosting.Modules.Application.Services;
//	using global::AutoMapper;
//	using global::AutoMapper.Extensions.ExpressionMapping;
//	using NUnit.Framework;

//	public class QueryOptionsTests
//	{
//		[Test]
//		public void ShouldMapQueryOptions_Sorting_SingleExpression()
//		{
//			MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddExpressionMapping().AddProfile<MapperProfile>());
//			IMapper mapper = config.CreateMapper();

//			IQueryOptions<CustomerDto> options = QueryOptions<CustomerDto>.OrderBy(x => x.Name);
//			Repository.Query.IQueryOptions<Customer> mappedOptions = options.MapQueryOptions<CustomerDto, Customer>(mapper);

//			mappedOptions.Should().NotBeNull();
//			mappedOptions.TryGetSortingOptions(out Repository.Query.ISortingOptions<Customer> mappedSortingOptions).Should().BeTrue();

//			mappedSortingOptions.PrimaryExpression.Should().NotBeNull();
//			mappedSortingOptions.PrimaryExpression.IsDescending.Should().BeFalse();
//			mappedSortingOptions.PrimaryExpression.Expression.Should().NotBeNull();
//		}

//		[Test]
//		public void ShouldMapQueryOptions_Sorting_MultipleExpressions()
//		{
//			MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddExpressionMapping().AddProfile<MapperProfile>());
//			IMapper mapper = config.CreateMapper();

//			IQueryOptions<CustomerDto> options = QueryOptions<CustomerDto>.OrderBy(x => x.Name).ThenByDescending(x => x.Number);
//			Repository.Query.IQueryOptions<Customer> mappedOptions = options.MapQueryOptions<CustomerDto, Customer>(mapper);

//			mappedOptions.Should().NotBeNull();
//			mappedOptions.TryGetSortingOptions(out Repository.Query.ISortingOptions<Customer> mappedSortingOptions).Should().BeTrue();

//			mappedSortingOptions.PrimaryExpression.Should().NotBeNull();
//			mappedSortingOptions.PrimaryExpression.IsDescending.Should().BeFalse();
//			mappedSortingOptions.PrimaryExpression.Expression.Should().NotBeNull();

//			mappedSortingOptions.SecondaryExpressions.Should().NotBeNullOrEmpty();
//			Repository.Query.ISortExpression<Customer> secondaryExpression = mappedSortingOptions.SecondaryExpressions.First();
//			secondaryExpression.Should().NotBeNull();
//			secondaryExpression.IsDescending.Should().BeTrue();
//			secondaryExpression.Expression.Should().NotBeNull();
//		}

//		[Test]
//		public void ShouldMapQueryOptions_SkipTake_WithoutSorting()
//		{
//			MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddExpressionMapping().AddProfile<MapperProfile>());
//			IMapper mapper = config.CreateMapper();

//			IQueryOptions<CustomerDto> options = QueryOptions<CustomerDto>.Skip(5).Take(10);
//			Repository.Query.IQueryOptions<Customer> mappedOptions = options.MapQueryOptions<CustomerDto, Customer>(mapper);

//			mappedOptions.Should().NotBeNull();
//			mappedOptions.TryGetSortingOptions(out _).Should().BeFalse();
//			mappedOptions.TryGetSkipTakeOptions(out Repository.Query.ISkipTakeOptions<Customer> mappedSkipTakeOptions).Should().BeTrue();

//			mappedSkipTakeOptions.Should().NotBeNull();
//			mappedSkipTakeOptions.SkipAmount.Should().HaveValue().And.Be(5);
//			mappedSkipTakeOptions.TakeAmount.Should().HaveValue().And.Be(10);
//		}

//		[Test]
//		public void ShouldMapQueryOptions_SkipTake_WithSorting()
//		{
//			MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddExpressionMapping().AddProfile<MapperProfile>());
//			IMapper mapper = config.CreateMapper();

//			IQueryOptions<CustomerDto> options = QueryOptions<CustomerDto>.OrderBy(x => x.Name).Skip(5).Take(10);
//			Repository.Query.IQueryOptions<Customer> mappedOptions = options.MapQueryOptions<CustomerDto, Customer>(mapper);

//			mappedOptions.Should().NotBeNull();
//			mappedOptions.TryGetSortingOptions(out _).Should().BeTrue();
//			mappedOptions.TryGetSkipTakeOptions(out Repository.Query.ISkipTakeOptions<Customer> mappedSkipTakeOptions).Should().BeTrue();

//			mappedSkipTakeOptions.Should().NotBeNull();
//			mappedSkipTakeOptions.SkipAmount.Should().HaveValue().And.Be(5);
//			mappedSkipTakeOptions.TakeAmount.Should().HaveValue().And.Be(10);
//		}

//		public class Customer : AggregateRoot<Customer, string>
//		{
//			public string Name { get; set; }

//			public int Number { get; set; }
//		}

//		public class CustomerDto : EntityDto<string>
//		{
//			public string Name { get; set; }

//			public int Number { get; set; }
//		}

//		public class MapperProfile : Profile
//		{
//			public MapperProfile()
//			{
//				this.CreateMap<Customer, CustomerDto>();
//			}
//		}
//	}
//}
