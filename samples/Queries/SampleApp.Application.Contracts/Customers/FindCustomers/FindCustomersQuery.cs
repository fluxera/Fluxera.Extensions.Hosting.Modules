namespace SampleApp.Application.Contracts.Customers.FindCustomers
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Queries;
	using Fluxera.Queries.Options;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class FindCustomersQuery : FindQuery
	{
		public FindCustomersQuery(QueryOptions queryOptions) 
			: base(queryOptions)
		{
		}
    }
}
