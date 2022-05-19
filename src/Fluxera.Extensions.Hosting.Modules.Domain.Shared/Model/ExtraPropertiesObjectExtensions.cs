namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model
{
	using System.Collections.Generic;
	using System.Dynamic;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="IExtraPropertiesObject" /> type.
	/// </summary>
	[PublicAPI]
	public static class ExtraPropertiesObjectExtensions
	{
		/// <summary>
		///     Gets the extra properties as dynamic instance.
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static dynamic ToDynamic(this IExtraPropertiesObject source)
		{
			IDictionary<string, object> extraProperties = source.ExtraProperties ?? new Dictionary<string, object>();
			ExpandoObject expandoObject = new ExpandoObject();
			ICollection<KeyValuePair<string, object>> collection = expandoObject;

			foreach(KeyValuePair<string, object> item in extraProperties)
			{
				collection.Add(item);
			}

			return expandoObject;
		}
	}
}
