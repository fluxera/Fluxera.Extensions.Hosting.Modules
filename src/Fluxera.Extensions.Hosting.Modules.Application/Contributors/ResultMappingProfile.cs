namespace Fluxera.Extensions.Hosting.Modules.Application.Contributors
{
	using System.Reflection;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using global::AutoMapper;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;

	[UsedImplicitly]
	internal sealed class ResultMappingProfile : Profile
	{
		public ResultMappingProfile()
		{
			this.CreateMap<IError, ErrorDto>();
			this.CreateMap<ISuccess, SuccessDto>();
			this.CreateMap(typeof(Result<>), typeof(ResultDto<>))
				.ForMember("Value", options =>
				{
					options.PreCondition(src =>
					{
						PropertyInfo property = src.GetType().GetProperty(nameof(Result.IsSuccessful));
						bool isSuccessful = (bool) (property?.GetValue(src) ?? false);
						return isSuccessful;
					});
				});
		}
	}
}
