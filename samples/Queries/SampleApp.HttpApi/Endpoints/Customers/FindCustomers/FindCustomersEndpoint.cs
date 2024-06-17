namespace SampleApp.HttpApi.Endpoints.Customers.FindCustomers
{
	using Fluxera.Queries.AspNetCore;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;
	using SampleApp.Application.Contracts.Customers;

	[PublicAPI]
    public sealed class FindCustomersEndpoint : EndpointBase
    {
        /// <inheritdoc />
        public override void Map(IEndpointRouteBuilder endpoints)
        {
            endpoints
                .MapFindQueryEndpoint<CustomerDto>()
                .AllowAnonymous();
        }
    }
}
