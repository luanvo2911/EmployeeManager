using AutoMapper;
using EmployeeManager.Entity.Entities;
using EmployeeManager.WebAPI.Model;

namespace EmployeeManager.WebAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Employee, EmployeeModel>().ForMember(
                dest => dest.fullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            );
            CreateMap<Vlogin, LoginModel>().ReverseMap();
        }
    }
}
