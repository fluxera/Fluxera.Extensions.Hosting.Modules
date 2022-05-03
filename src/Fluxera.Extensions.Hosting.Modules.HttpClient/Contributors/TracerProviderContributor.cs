namespace Fluxera.Extensions.Hosting.Modules.HttpClient.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using global::OpenTelemetry.Trace;

	internal sealed class TracerProviderContributor : ITracerProviderContributor
	{
		/// <inheritdoc />
		public void Configure(TracerProviderBuilder builder)
		{
			builder.AddHttpClientInstrumentation(options =>
			{
				//options.Enrich = (activity, eventName, rawObject) =>
				//{
				//	if(eventName.Equals("OnStartActivity"))
				//	{
				//		if(rawObject is HttpRequestMessage request)
				//		{
				//			activity.SetTag("requestVersion", request.Version);
				//		}
				//	}
				//	else if(eventName.Equals("OnStopActivity"))
				//	{
				//		if(rawObject is HttpResponseMessage response)
				//		{
				//			activity.SetTag("responseVersion", response.Version);
				//		}
				//	}
				//	else if(eventName.Equals("OnException"))
				//	{
				//		if(rawObject is Exception exception)
				//		{
				//			activity.SetTag("stackTrace", exception.StackTrace);
				//		}
				//	}
				//};
			});
		}
	}
}
