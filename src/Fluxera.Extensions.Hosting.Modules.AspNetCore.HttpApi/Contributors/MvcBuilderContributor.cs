namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Contributors
{
	using MadEyeMatt.AspNetCore.ProblemDetails;
	using Microsoft.Extensions.DependencyInjection;
	using Controllers = SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions.ServiceCollectionExtensions;
	using Endpoints = SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions.ServiceCollectionExtensions;

	internal sealed class MvcBuilderContributor : IMvcBuilderContributor
	{
		/// <inheritdoc />
		public void Configure(IMvcBuilder builder, IServiceConfigurationContext context)
		{
			context.Log("AddProblemDetails", _ => builder.AddProblemDetails());

			context.Log("AddFluentValidationAutoValidation", services =>
			{
				// https://github.com/SharpGrip/FluentValidation.AutoValidation
				Controllers.AddFluentValidationAutoValidation(services);
				Endpoints.AddFluentValidationAutoValidation(services);
			});
		}
	}
}
