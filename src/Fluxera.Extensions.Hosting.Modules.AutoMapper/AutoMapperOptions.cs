namespace Fluxera.Extensions.Hosting.Modules.AutoMapper
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using global::AutoMapper;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     The options for the AutoMapper module.
	/// </summary>
	[PublicAPI]
	public sealed class AutoMapperOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="AutoMapperOptions" /> type.
		/// </summary>
		public AutoMapperOptions()
		{
			this.Configurators = new List<Action<AutoMapperConfigurationContext>>();
			this.ValidatingProfiles = new TypeList<Profile>();
		}

		internal IList<Action<AutoMapperConfigurationContext>> Configurators { get; }

		internal ITypeList<Profile> ValidatingProfiles { get; }

		/// <summary>
		///     Add the profile to the mapper configuration.
		/// </summary>
		/// <typeparam name="TProfile"></typeparam>
		/// <param name="validate"></param>
		/// <returns></returns>
		public AutoMapperOptions AddProfile<TProfile>(bool validate = false) where TProfile : Profile
		{
			this.Configurators.Add(context =>
			{
				// https://jan-v.nl/post/using-dependency-injection-in-your-automapper-profile/
				TProfile profile = context.ServiceProvider.GetService<TProfile>();
				if(profile != null)
				{
					context.MapperConfiguration.AddProfile(profile);
				}
				else
				{
					context.MapperConfiguration.AddProfile(typeof(TProfile));
				}
			});

			if(validate)
			{
				this.ValidateProfile(typeof(TProfile));
			}

			return this;
		}

		internal void ValidateProfile<TProfile>(bool validate = true) where TProfile : Profile
		{
			this.ValidateProfile(typeof(TProfile), validate);
		}

		internal void ValidateProfile(Type profileType, bool validate = true)
		{
			if(validate)
			{
				this.ValidatingProfiles.AddIfNotContains(profileType);
			}
			else
			{
				this.ValidatingProfiles.Remove(profileType);
			}
		}
	}
}
