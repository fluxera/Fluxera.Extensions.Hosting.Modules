namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using System.Collections.Generic;
	using FluentValidation;
	using FluentValidation.Results;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc.ModelBinding;

	/// <summary>
	///     Extensions methods for the <see cref="ModelStateDictionary" /> type.
	/// </summary>
	[PublicAPI]
	public static class ModelStateDictionaryExtensions
	{
		/// <summary>
		///     Adds a single, model-scoped error message.
		/// </summary>
		/// <param name="modelState"></param>
		/// <param name="message"></param>
		public static void AddModelError(this ModelStateDictionary modelState, string message)
		{
			modelState.AddModelError(string.Empty, message);
		}

		/// <summary>
		///     Adds the available validation errors from the given <see cref="ValidationException" />.
		/// </summary>
		/// <param name="modelState"></param>
		/// <param name="ex"></param>
		public static void AddModelErrors(this ModelStateDictionary modelState, ValidationException ex)
		{
			modelState.AddModelErrors(ex.Errors);
		}

		private static void AddModelErrors(this ModelStateDictionary modelState, IEnumerable<ValidationFailure> errors)
		{
			foreach(ValidationFailure error in errors)
			{
				modelState.AddModelError(error.PropertyName ?? string.Empty, error.ErrorMessage);
			}
		}
	}
}
