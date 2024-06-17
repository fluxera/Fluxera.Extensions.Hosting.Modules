namespace SampleApp.Application.Customers.GetCustomerCount
{
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Fluxera.Extensions.Hosting.Modules.Application.Handlers;
	using Fluxera.Repository.Queries;
	using Fluxera.Results;
	using JetBrains.Annotations;
	using SampleApp.Application.Contracts.Customers;
	using SampleApp.Application.Contracts.Customers.GetCustomerCount;
	using SampleApp.Domain.Customers;
	using SampleApp.Domain.Shared.Customers;

	[UsedImplicitly]
	internal sealed class GetCustomerCountQueryHandler : IApplicationQueryHandler<GetCustomerCountQuery, long>
	{
		private readonly ICustomerRepository customerRepository;
		private readonly IMapper mapper;

		public GetCustomerCountQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
		{
			this.mapper = mapper;
			this.customerRepository = customerRepository;
		}

		/// <inheritdoc />
		public async Task<Result<long>> HandleAsync(GetCustomerCountQuery query, CancellationToken cancellationToken)
		{
			long count = await this.customerRepository.ExecuteCountAsync<Customer, CustomerId, CustomerDto>(
				query.QueryOptions, this.mapper, cancellationToken);

			return Result.Ok(count);
		}
	}
}
