namespace SampleApp.Infrastructure.Contributors
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Bogus;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.DataManagement;
	using Fluxera.Repository;
	using Fluxera.Utilities.Extensions;
	using Microsoft.Extensions.DependencyInjection;
	using SampleApp.Domain.Customers;
	using SampleApp.Domain.Shared.Customers;

	internal sealed class DataSeedingContributor : IDataSeedingContributor
	{
		/// <inheritdoc />
		public async Task ExecuteAsync(IApplicationInitializationContext context)
		{
			using (IServiceScope serviceScope = context.ServiceProvider.CreateScope())
			{
				IRepository<Customer, CustomerId> repository = serviceScope.ServiceProvider.GetRequiredService<IRepository<Customer, CustomerId>>();
				IUnitOfWorkFactory unitOfWorkFactory = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory>();
				IUnitOfWork unitOfWork = unitOfWorkFactory.CreateUnitOfWork(repository.RepositoryName);

				if(await repository.CountAsync() > 0)
				{
					return;
				}

				Faker<Country> countryFaker = new Faker<Country>()
					.UseSeed(37)
					.CustomInstantiator(x =>
					{
						string code = x.Address.CountryCode();
						string name = x.Address.Country();

						return new Country
						{
							Code = code,
							Name = name,
						};
					});

				Faker<Address> addressFaker = new Faker<Address>()
					.UseSeed(37)
					.CustomInstantiator(x =>
					{
						string street = x.Address.StreetName();
						string number = x.Address.BuildingNumber();
						string city = x.Address.City();
						string zipCode = x.Address.ZipCode("#####");

						return new Address
						{
							Street = street,
							Number = number,
							City = city,
							ZipCode = new ZipCode(zipCode),
							Country = countryFaker.Generate(1).First()
						};
					});

				Faker<Customer> customerFaker = new Faker<Customer>()
				   .UseSeed(37)
				   .CustomInstantiator(x =>
				   {
					   string firstName = x.Name.FirstName();
					   string lastName = x.Name.LastName();
					   string email = x.Internet.Email(firstName, lastName);

					   DateTime today = DateTime.Today;
					   DateTime dateOfBirth = x.Person.DateOfBirth;
					   int age = today.Year - dateOfBirth.Year;

					   CustomerState state = Random.Shared.Next(0, 10).IsEven() ? CustomerState.New : CustomerState.Legacy;

					   return new Customer
					   {
						   FirstName = firstName,
						   LastName = lastName,
						   Email = email,
						   Age = new Age(age),
						   State = state,
						   Address = addressFaker.Generate(1).First()
					   };
				   });

				int counter = 0;
				foreach(Customer customer in customerFaker.GenerateForever())
				{
					counter++;

					await repository.AddAsync(customer);

					if(counter == 100)
					{
						break;
					}
				}

				await unitOfWork.SaveChangesAsync();
			}
		}
	}
}
