namespace Fluxera.Extensions.Hosting.Modules.Application.Behaviors
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Threading;
	using System.Threading.Tasks;
	using FluentValidation;
	using FluentValidation.Results;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	/// <summary>
	///		A MediatR pipeline behavior that validates request instances.
	/// </summary>
	/// <typeparam name="TRequest"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	[UsedImplicitly]
	internal sealed class ValidationPipelineBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
		where TRequest : IRequest<TResult>
	{
		private readonly IEnumerable<IValidator<TRequest>> validators;

		/// <summary>
		///		Initializes a new instance of the <see cref="ValidationPipelineBehavior{TRequest, TResult}"/> type.
		/// </summary>
		/// <param name="validators"></param>
		public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			this.validators = validators;
		}

		/// <inheritdoc />
		public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
		{
			if (!this.validators.Any())
			{
				return await next();
			}

			ValidationFailure[] validationFailures = this.validators
				.Select(validator => validator.Validate(request))
				.SelectMany(validationResult => validationResult.Errors)
				.Where(validationFailure => validationFailure is not null)
				.ToArray();

			Type resultType = typeof(TResult);
			bool isGenericType = resultType.IsGenericType;

			switch(isGenericType)
			{
				case false when resultType != typeof(Result):
				case true when resultType.GetGenericTypeDefinition() != typeof(Result<>):
					ThrowOnValidationFailed(validationFailures);
					break;
			}

			ValidationError[] errors = validationFailures
			   .Select(x => new ValidationError(x.PropertyName, x.ErrorMessage))
			   .Distinct()
			   .ToArray();

			if (errors.Any())
			{
				return CreateValidationResult(errors);
			}

			return await next();
		}

		private static void ThrowOnValidationFailed(ValidationFailure[] validationFailures)
		{
			if(validationFailures.Any())
			{
				throw new ValidationException(validationFailures);
			}
		}

		private static TResult CreateValidationResult(ValidationError[] errors)
		{
			Type resultType = typeof(TResult);

			if (resultType == typeof(Result))
			{
				return (TResult)(object)Result.Fail(errors);
			}

			object result = typeof(Result<>)
							.GetGenericTypeDefinition()
							.MakeGenericType(resultType.GenericTypeArguments[0])
							.CreateInstance();

			result.GetType()
				  .GetMethod(
					  nameof(Result.WithErrors), 
					  BindingFlags.Instance | BindingFlags.Public,
					  [typeof(IEnumerable<IError>)])?
				.Invoke(result, [errors]);

			return (TResult)result;
		}
	}
}
