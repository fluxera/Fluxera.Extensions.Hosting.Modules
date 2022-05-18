namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model
{
	using System;
	using System.Globalization;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="IExtraPropertiesObject" /> type.
	/// </summary>
	[PublicAPI]
	public static class ExtraPropertiesObjectExtensions
	{
		public static bool HasProperty(this IExtraPropertiesObject source, string name, bool camelize = true)
		{
			return source.ExtraProperties.ContainsKey(camelize ? name.Camelize() : name);
		}

		public static object GetProperty(this IExtraPropertiesObject source, string name, bool camelize = true)
		{
			return source.ExtraProperties?.GetOrDefault(camelize ? name.Camelize() : name);
		}

		public static TProperty GetProperty<TProperty>(this IExtraPropertiesObject source, string name, bool camelize = true)
		{
			object value = source.GetProperty(name, camelize);
			if(value == default)
			{
				return default;
			}

			if(typeof(TProperty).IsPrimitive(true))
			{
				return (TProperty)Convert.ChangeType(value, typeof(TProperty), CultureInfo.InvariantCulture);
			}

			throw new NotSupportedException("Non-primitive types are not supported.");
		}

		public static TSource SetProperty<TSource>(this TSource source, string name, object value, bool camelize = true)
			where TSource : IExtraPropertiesObject
		{
			source.ExtraProperties[camelize ? name.Camelize() : name] = value;
			return source;
		}

		public static TSource RemoveProperty<TSource>(this TSource source, string name, bool camelize = true)
			where TSource : IExtraPropertiesObject
		{
			source.ExtraProperties.Remove(camelize ? name.Camelize() : name);
			return source;
		}
	}
}
