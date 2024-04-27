namespace Ordering.HttpApi.Endpoints.Orders
{
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Routing;

	[PublicAPI]
	public sealed class GetOrders : EndpointBase
	{
		/// <inheritdoc />
		public override void Map(IEndpointRouteBuilder endpoints)
		{
			endpoints
				.MapGet(this.Execute)
				.AllowAnonymous();
		}

		private Task<IResult> Execute(CancellationToken cancellationToken = default)
		{
			return Task.FromResult(Results.Ok());
		}
	}
}
