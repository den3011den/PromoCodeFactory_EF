using AutoMapper;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;
using System.Linq;

namespace DictionaryManagement_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleItemResponse>().ReverseMap();

            CreateMap<PreferenceResponse, Preference>().ReverseMap();

            CreateMap<EmployeeResponse, Employee>().ReverseMap();

            //CreateMap<EmployeeResponse, Employee>()
            //    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            //CreateMap<Employee, EmployeeResponse>()
            //    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<EmployeeShortResponse, Employee>().ReverseMap();

            //CreateMap<CustomerResponse, Customer>()
            //    .ForMember(dest => dest.PromoCodes, opt => opt.MapFrom(src => src.PromoCodes))
            //    .ForMember(dest => dest.Preferences, opt => opt.MapFrom(src => src.Preferences));

            //CreateMap<Customer, CustomerResponse>()
            //    .ForMember(dest => dest.PromoCodes, opt => opt.MapFrom(src => src.PromoCodes))
            //    .ForMember(dest => dest.Preferences, opt => opt.MapFrom(src => src.Preferences));

            CreateMap<CustomerResponse, Customer>().ReverseMap();

            CreateMap<CustomerShortResponse, Customer>().ReverseMap();

            CreateMap<PromoCodeShortResponse, PromoCode>().ReverseMap();

            CreateMap<GivePromoCodeRequest, PromoCode>().ReverseMap();

            CreateMap<Customer, CreateOrEditCustomerRequest>()
                .ForMember(dest => dest.PreferenceIds, opt => opt.MapFrom(src => src.Preferences.Select(x => x.Id).ToList()));
        }
    }
}
