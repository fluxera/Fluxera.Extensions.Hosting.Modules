namespace BlazorWasmPerPageComponent.Client
{
	using Fluxera.Extensions.Hosting;
	using System.Threading.Tasks;

	internal static class Program
	{
		public static async Task Main(string[] args)
		{
			await ApplicationHost.RunAsync<BlazorWasmPerPageComponentClientHost>(args);
		}
	}
}
