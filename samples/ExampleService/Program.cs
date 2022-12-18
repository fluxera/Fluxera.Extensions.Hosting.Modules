namespace CatalogService
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting;

	public static class Program
	{
		public static async Task Main(string[] args)
		{
			// https://github.com/jbogard/MediatR/wiki
			// https://github.com/altmann/FluentResults

			await ApplicationHost.RunAsync<ExampleServiceHost>(args);
		}
	}
}
