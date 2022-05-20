namespace Example.Application.Contracts.Services
{
	using System.Threading.Tasks;
	using Example.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for application service implementations for client and service side.
	/// </summary>
	[PublicAPI]
	public interface IExampleApplicationService
	{
		Task<ExampleDto> GetExampleAsync(string id);

		Task<ExampleDto> AddExample(ExampleDto item);
	}
}
