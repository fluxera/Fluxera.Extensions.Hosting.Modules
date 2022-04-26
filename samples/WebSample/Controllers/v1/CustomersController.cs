namespace WebSample.Controllers.v1
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Asp.Versioning;
	using Fluxera.Repository;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using WebSample.Model;

	[ApiController]
	[ApiVersion("1.0")]
	[Route("v{version:apiVersion}/[controller]")]
	public class CustomersController : ControllerBase
	{
		private readonly ILogger<CustomersController> logger;
		private readonly IRepository<Customer, string> repository;

		public CustomersController(
			ILogger<CustomersController> logger,
			IRepository<Customer, string> repository)
		{
			this.logger = logger;
			this.repository = repository;
		}

		/// <summary>
		///     Get a customer by ID.
		/// </summary>
		/// <param name="id">The customer ID.</param>
		/// <returns>The customer instance.</returns>
		[HttpGet("{id}")]
		[MapToApiVersion(1.0)]
		[AllowAnonymous]
		public async Task<IActionResult> GetAsync(string id)
		{
			Customer customer = await this.repository.FindOneAsync(x => x.ID == id);
			return this.Ok(customer);
		}

		[HttpGet("by-number/{number}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAsync(int number)
		{
			Customer customer = await this.repository.FindOneAsync(x => x.Number == number);
			return this.Ok(customer);
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> GetAsync()
		{
			IReadOnlyCollection<Customer> customers = await this.repository.FindManyAsync(x => true);
			return this.Ok(customers);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> PostAsync()
		{
			Customer customer = new Customer
			{
				Name = Guid.NewGuid().ToString("N"),
				Number = Random.Shared.Next(0, int.MaxValue)
			};
			await this.repository.AddAsync(customer);

			this.logger.LogInformation("Customer added: ID={CustomerID}", customer.ID);

			return this.Ok(customer);
		}

		[HttpDelete("{id}")]
		[AllowAnonymous]
		public async Task<IActionResult> DeleteAsync([FromRoute] string id)
		{
			await this.repository.RemoveAsync(id);

			this.logger.LogInformation("Customer deleted: ID={CustomerID}", id);

			return this.NoContent();
		}
	}
}
