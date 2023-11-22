namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System.Threading.Tasks;
	using FluentValidation;
	using FluentValidation.Results;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;
	using SharpGrip.FluentValidation.AutoValidation.Endpoints.Results;
	using SharpGrip.FluentValidation.AutoValidation.Shared.Extensions;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class CustomFluentValidationAutoValidationEndpointFilter : IEndpointFilter
	{
		/// <inheritdoc />
		public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
		{
			for(int i = 0; i < context.Arguments.Count; i++)
			{
				object argument = context.Arguments[i];

				if(argument != null && argument.GetType().IsCustomType() && context.HttpContext.RequestServices.GetValidator(argument.GetType()) is IValidator validator)
				{
					ValidationResult validationResult = await validator.ValidateAsync(new ValidationContext<object>(argument), context.HttpContext.RequestAborted);

					if(!validationResult.IsValid)
					{
						IFluentValidationAutoValidationResultFactory fluentValidationAutoValidationResultFactory = context.HttpContext.RequestServices.GetService<IFluentValidationAutoValidationResultFactory>();

						if(fluentValidationAutoValidationResultFactory != null)
						{
							return fluentValidationAutoValidationResultFactory.CreateResult(context, validationResult);
						}

						return new FluentValidationAutoValidationDefaultResultFactory().CreateResult(context, validationResult);
					}
				}
			}

			return await next(context);
		}
	}
}
