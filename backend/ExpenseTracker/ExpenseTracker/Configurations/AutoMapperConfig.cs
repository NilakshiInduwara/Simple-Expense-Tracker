using AutoMapper;
using ExpenseTracker.Entities;
using ExpenseTracker.Models;

namespace ExpenseTracker.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() {
            CreateMap<User, UserDTO>().ForMember(n => n.UserName, opt => opt.MapFrom(x => x.Name)).ReverseMap();

            /*CreateMap<User, UserDTO>()
                .ForMember(n => n.Email, opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Email) ? "No Email Found" : n.Email))
                .ReverseMap();*/

            CreateMap<Expense, ExpenseDTO>().ReverseMap();
        }
    }
}
