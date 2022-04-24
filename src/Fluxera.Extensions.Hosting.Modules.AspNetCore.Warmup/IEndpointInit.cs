namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup
{
	using System.Threading.Tasks;

	internal interface IEndpointInit
	{
		Task InitializeEndpointsAsync();
	}
}
