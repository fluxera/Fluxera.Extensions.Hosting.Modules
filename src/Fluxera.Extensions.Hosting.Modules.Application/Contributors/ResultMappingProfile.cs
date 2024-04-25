namespace Fluxera.Extensions.Hosting.Modules.Application.Contributors
{
	using System;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos.Results;
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
			this.CreateMap<Result, ResultDto>();
			this.CreateMap(typeof(Result<>), typeof(ResultDto<>))
				.ConstructUsing((src, _) =>
				{
					Type valueType = src.GetType().GetGenericArguments().Single();
					Type resultDtoType = typeof(ResultDto<>).MakeGenericType(valueType);

					return Activator.CreateInstance(resultDtoType);
				})
				.ForMember("Value", o => o.PreCondition(src =>
				{
					MethodInfo method = src.GetType().GetMethod("GetValueOrDefault");
					object value = method?.Invoke(src, [default]);

					return value != null;
				}));
		}
	}
}
