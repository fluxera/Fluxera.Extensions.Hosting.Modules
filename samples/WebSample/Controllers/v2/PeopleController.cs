//namespace WebSample.Controllers.v2
//{
//	using System.Threading.Tasks;
//	using Fluxera.Repository;
//	using Microsoft.AspNetCore.Mvc;
//	using Microsoft.AspNetCore.OData.Query;
//	using Microsoft.AspNetCore.OData.Routing.Controllers;
//	using WebSample.Model;

//	[ApiVersion("2.0")]
//	public class PeopleController : ODataController
//	{
//		private readonly IRepository<Person, string> repository;

//		public PeopleController(IRepository<Person, string> repository)
//		{
//			this.repository = repository;
//		}

//		[EnableQuery]
//		public async Task<IActionResult> Get(string key)
//		{
//			Person person = await this.repository.GetAsync(key);
//			if(person == null)
//			{
//				return this.NotFound($"Cannot find person with ID={key}.");
//			}

//			return this.Ok(person);
//		}
//	}
//}


