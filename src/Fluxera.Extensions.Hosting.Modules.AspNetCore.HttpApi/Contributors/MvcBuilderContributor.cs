namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Contributors
{
	using MadEyeMatt.AspNetCore.ProblemDetails;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class MvcBuilderContributor : IMvcBuilderContributor
	{
		/// <inheritdoc />
		public void Configure(IMvcBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddProblemDetails();
		}
	}
}
