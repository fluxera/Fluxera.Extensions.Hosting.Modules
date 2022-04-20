namespace WebApplication1.Controllers
{
	using Fluxera.Repository;
	using Microsoft.AspNetCore.Mvc;
	using WebApplication1.Model;

	[ApiController]
	[Route("customers")]
	public class CustomerController : ControllerBase
	{
		private readonly ILogger<CustomerController> logger;
		private readonly IRepository<Customer, string> repository;

		public CustomerController(
			IRepository<Customer, string> repository,
			ILogger<CustomerController> logger)
		{
			this.repository = repository;
			this.logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{
			IReadOnlyCollection<Customer> customers = await this.repository.FindManyAsync(x => true);
			return this.Ok(customers);
		}

		[HttpPost]
		public async Task<IActionResult> PostAsync()
		{
			Customer customer = new Customer
			{
				Name = Guid.NewGuid().ToString("N")
			};
			await this.repository.AddAsync(customer);
			return this.Ok(customer);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync([FromRoute] string id)
		{
			await this.repository.RemoveAsync(id);
			return this.NoContent();
		}
	}
}
