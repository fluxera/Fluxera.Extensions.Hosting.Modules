namespace SampleApp.Application.Customers.FindCustomers
{
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Fluxera.Extensions.Hosting.Modules.Application.Handlers;
	using Fluxera.Queries;
	using Fluxera.Repository.Queries;
	using Fluxera.Repository.Query;
	using Fluxera.Results;
	using JetBrains.Annotations;
	using SampleApp.Application.Contracts.Customers;
	using SampleApp.Application.Contracts.Customers.FindCustomers;
	using SampleApp.Domain.Customers;
	using SampleApp.Domain.Shared.Customers;

	[UsedImplicitly]
	internal sealed class FindCustomersQueryHandler : IApplicationQueryHandler<FindCustomersQuery, QueryResult>
	{
        private readonly ICustomerRepository customerRepository;
		private readonly IMapper mapper;
		private readonly IQueryOptionsBuilder<Customer> queryOptionsBuilder;

		public FindCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper, IQueryOptionsBuilder<Customer> queryOptionsBuilder)
        {
			this.mapper = mapper;
			this.queryOptionsBuilder = queryOptionsBuilder;
			this.customerRepository = customerRepository;
        }

		/// <inheritdoc />
		public async Task<Result<QueryResult>> HandleAsync(FindCustomersQuery query, CancellationToken cancellationToken)
		{
			QueryResult result = await this.customerRepository.ExecuteFindManyAsync<Customer, CustomerId, CustomerDto>(
				query.QueryOptions, this.mapper, this.queryOptionsBuilder, cancellationToken);

			return Result.Ok(result);
		}
	}
}
