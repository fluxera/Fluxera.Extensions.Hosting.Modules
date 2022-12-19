namespace OrderingService
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting;

	internal static class Program
	{
		public static async Task Main(string[] args)
		{
			// https://github.com/jbogard/MediatR/wiki
			// https://github.com/altmann/FluentResults

			await ApplicationHost.RunAsync<OrderingServiceHost>(args);
		}
	}
}
