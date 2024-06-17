namespace SampleApp.Application.Contracts.Customers.GetCustomerCount
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Queries;
	using Fluxera.Queries.Options;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class GetCustomerCountQuery : CountQuery
	{
		public GetCustomerCountQuery(QueryOptions queryOptions) 
			: base(queryOptions)
		{
		}
	}
}
