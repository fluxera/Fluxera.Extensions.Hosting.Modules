﻿namespace ConsoleSample
{
	using Fluxera.Extensions.Hosting;

	public static class Program
	{
		public static async Task Main(string[] args)
		{
			await ApplicationHost.RunAsync<ConsoleSampleHost>(args);

			Console.WriteLine();
			Console.WriteLine("Press any key to quit...");
			Console.ReadKey(true);
		}
	}
}