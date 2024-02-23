namespace BlazorWasmGlobal.Client
{
	using Fluxera.Extensions.Hosting;

	internal static class Program
	{
		public static async Task Main(string[] args)
		{
			await ApplicationHost.RunAsync<BlazorWasmGlobalClientHost>(args);
		}
	}
}
