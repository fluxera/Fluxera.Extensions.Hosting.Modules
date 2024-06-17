namespace SampleApp.Application.Contracts.Customers.GetCustomer
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Queries;
	using Fluxera.Queries.Options;
	using JetBrains.Annotations;
	using SampleApp.Domain.Shared.Customers;

	[PublicAPI]
	public sealed class GetCustomerQuery : GetQuery<CustomerId>
	{
		public GetCustomerQuery(CustomerId customerId, QueryOptions queryOptions) 
			: base(customerId, queryOptions)
		{
		}
	}
}
