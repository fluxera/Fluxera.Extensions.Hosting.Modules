namespace WebSample.Controllers.v2
{
	using System;
	using System.Collections.Generic;
	using System.Net.Mime;
	using System.Threading.Tasks;
	using Fluxera.Repository;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using WebSample.Model;

	/// <summary>
	///     The customer endpoints.
	/// </summary>
	[ApiController]
	[ApiVersion("2.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
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
		[Obsolete]
		[HttpGet("{id}")]
		[AllowAnonymous]
		[MapToApiVersion("2.0")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json, Type = typeof(Customer))]
		[AcceptVerbs]
		public async Task<IActionResult> GetAsync(string id)
		{
			Customer customer = await this.repository.FindOneAsync(x => x.ID == id);
			return this.Ok(customer);
		}

		[MapToApiVersion("2.0")]
		[HttpGet("by-number/{number}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAsync(int number)
		{
			Customer customer = await this.repository.FindOneAsync(x => x.Number == number);
			return this.Ok(customer);
		}

		[MapToApiVersion("2.0")]
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> GetAsync()
		{
			IReadOnlyCollection<Customer> customers = await this.repository.FindManyAsync(x => true);
			return this.Ok(customers);
		}

		[MapToApiVersion("2.0")]
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> PostAsync([FromBody] Customer customer)
		{
			await this.repository.AddAsync(customer);

			this.logger.LogInformation("Customer added: ID={CustomerID}", customer.ID);

			return this.Ok(customer);
		}

		[MapToApiVersion("2.0")]
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
