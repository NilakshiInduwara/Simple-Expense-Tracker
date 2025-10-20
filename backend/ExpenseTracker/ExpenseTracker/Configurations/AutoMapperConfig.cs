using AutoMapper;
using ExpenseTracker.Entities;
using ExpenseTracker.Models;

namespace ExpenseTracker.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() { 
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Expense, ExpenseDTO>().ReverseMap();
        }
    }
}
