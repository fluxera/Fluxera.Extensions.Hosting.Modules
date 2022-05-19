namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Entity;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="Assembly" /> type.
	/// </summary>
	[PublicAPI]
	public static class AssemblyExtensions
	{
		/// <summary>
		///     Enumerates all available aggregate root types available in the given assembly.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static IEnumerable<Type> EnumerateAggregates(this Assembly assembly)
		{
			Guard.Against.Null(assembly);

			Type[] types = assembly.GetTypes();
			foreach(Type type in types)
			{
				if(type.IsAggregateRoot())
				{
					yield return type;
				}
			}
		}
	}
}
