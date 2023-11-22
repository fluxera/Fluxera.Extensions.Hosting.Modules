namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc.ModelBinding;
	using SharpGrip.FluentValidation.AutoValidation.Shared.Extensions;

	/// <summary>
	///		An endpoint filter that validates models using data annotations validation.
	/// </summary>
	[PublicAPI]
	public sealed class DataAnnotationsValidationEndpointFilter : IEndpointFilter
	{
		/// <inheritdoc />
		public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
		{
			foreach(object argument in context.Arguments)
			{
				if(argument != null && argument.GetType().IsCustomType())
				{
					ICollection<ValidationResult> validationResults = new List<ValidationResult>();

					if(!Validator.TryValidateObject(argument, new ValidationContext(argument), validationResults))
					{
						ModelStateDictionary modelState = new ModelStateDictionary();
						foreach(ValidationResult validationResult in validationResults)
						{
							if(validationResult.MemberNames.Any())
							{
								foreach(string memberName in validationResult.MemberNames)
								{
									modelState.AddModelError(memberName, validationResult.ErrorMessage ?? string.Empty);
								}
							}
							else
							{
								modelState.AddModelError(string.Empty, validationResult.ErrorMessage ?? string.Empty);
							}

							IDictionary<string, string[]> errorList = modelState
								.Where(x => x.Value?.Errors.Count > 0)
								.ToDictionary(
									kvp => kvp.Key,
									kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? Array.Empty<string>()
								);

							return TypedResults.ValidationProblem(errorList);
						}
					}
				}
			}

			return await next(context);
		}
	}
}
