namespace SampleApp.HttpApi.Endpoints.Customers.GetCustomer
{
	using Fluxera.Queries.AspNetCore;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;
	using SampleApp.Application.Contracts.Customers;

	[PublicAPI]
	public sealed class GetCustomerEndpoint : EndpointBase
    {
        /// <inheritdoc />
        public override void Map(IEndpointRouteBuilder endpoints)
        {
            endpoints
                .MapGetQueryEndpoint<CustomerDto>()
                .AllowAnonymous();
        }
    }
}
