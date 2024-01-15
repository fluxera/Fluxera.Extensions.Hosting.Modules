namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A DTO for a result.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ResultDto : ResultBaseDto<ResultDto>
	{
	}

	/// <summary>
	///     A DTO for a result with a value.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ResultDto<TValue> : ResultBaseDto<ResultDto<TValue>, TValue>
	{
	}
}
