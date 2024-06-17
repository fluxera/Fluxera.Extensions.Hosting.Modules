namespace SampleApp.Application.Mappings
{
	using AutoMapper;
	using JetBrains.Annotations;
	using SampleApp.Application.Contracts.Customers;
	using SampleApp.Domain.Customers;

	[UsedImplicitly]
	internal sealed class CustomerProfile : Profile
	{
		public CustomerProfile()
		{
			this.CreateMap<Customer, CustomerDto>()
				.ReverseMap()
				.ValidateMemberList(MemberList.Source);

			this.CreateMap<Country, CountryDto>()
				.ReverseMap()
				.ValidateMemberList(MemberList.Source);

			this.CreateMap<Address, AddressDto>()
				.ReverseMap()
				.ValidateMemberList(MemberList.Source);
		}
	}
}
