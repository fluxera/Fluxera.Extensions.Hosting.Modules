namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using System.Collections.Generic;
	using Fluxera.Extensions.Validation;
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

		private static void AddModelErrors(this ModelStateDictionary modelState, IEnumerable<ValidationError> errors)
		{
			foreach(ValidationError error in errors)
			{
				foreach(string errorMessage in error.ErrorMessages)
				{
					modelState.AddModelError(error.PropertyName ?? string.Empty, errorMessage);
				}
			}
		}
	}
}
