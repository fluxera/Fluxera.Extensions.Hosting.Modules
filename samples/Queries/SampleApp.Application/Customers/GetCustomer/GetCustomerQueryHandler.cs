namespace SampleApp.Application.Customers.GetCustomer
{
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Fluxera.Extensions.Hosting.Modules.Application.Handlers;
	using Fluxera.Queries;
	using Fluxera.Repository.Queries;
	using Fluxera.Results;
	using JetBrains.Annotations;
	using SampleApp.Application.Contracts.Customers;
	using SampleApp.Application.Contracts.Customers.GetCustomer;
	using SampleApp.Domain.Customers;
	using SampleApp.Domain.Shared.Customers;

	[UsedImplicitly]
	internal sealed class GetCustomerQueryHandler : IApplicationQueryHandler<GetCustomerQuery, SingleResult>
	{
		private readonly ICustomerRepository customerRepository;
		private readonly IMapper mapper;

		public GetCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
		{
			this.mapper = mapper;
			this.customerRepository = customerRepository;
		}

		/// <inheritdoc />
		public async Task<Result<SingleResult>> HandleAsync(GetCustomerQuery query, CancellationToken cancellationToken)
		{
			SingleResult result = await this.customerRepository.ExecuteGetAsync<Customer, CustomerId, CustomerDto>(
				query.ID, query.QueryOptions, this.mapper, cancellationToken);

			return Result.Ok(result);
		}
	}
}
