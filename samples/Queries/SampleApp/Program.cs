namespace SampleApp
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting;

	public static class Program
	{
		public static Task Main(string[] args)
		{
			return ApplicationHost.RunAsync<SampleAppServiceHost>(args);
		}
	}
}
