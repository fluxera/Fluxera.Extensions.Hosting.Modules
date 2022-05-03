namespace WebSample
{
	using Fluxera.Extensions.Hosting;
	using IdentityService;

	public static class Program
	{
		public static async Task Main(string[] args)
		{
			await ApplicationHost.RunAsync<IdentityServiceHost>(args);
		}
	}
}
