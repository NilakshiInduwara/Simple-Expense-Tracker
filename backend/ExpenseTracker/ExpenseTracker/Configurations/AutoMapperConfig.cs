using AutoMapper;
using ExpenseTracker.Entities;
using ExpenseTracker.Models;

namespace ExpenseTracker.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() { 
            CreateMap<User, UserDTO>().ForMember(n => n.UserName, opt => opt.MapFrom(x => x.Name)).ReverseMap();
            CreateMap<Expense, ExpenseDTO>().ReverseMap();
        }
    }
}
