namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic
{
	using System.Threading.Tasks;
	using global::AspNetCore.Authentication.Basic;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class BasicUserValidationService : IBasicUserValidationService
	{
		/// <inheritdoc />
		public async Task<bool> IsValidAsync(string username, string password)
		{
			// TODO
			return false;
		}
	}
}
