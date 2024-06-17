namespace SampleApp.HttpApi.Endpoints.Customers.GetCustomerCount
{
	using Fluxera.Queries.AspNetCore;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;
	using SampleApp.Application.Contracts.Customers;

	[PublicAPI]
	public sealed class GetCustomerCountEndpoint : EndpointBase
    {
        /// <inheritdoc />
        public override void Map(IEndpointRouteBuilder endpoints)
        {
			endpoints
				.MapCountQueryEndpoint<CustomerDto>()
				.AllowAnonymous();
		}
    }
}
