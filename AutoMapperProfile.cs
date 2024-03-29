using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using my_expense_api.Dtos.Request;
using my_expense_api.Dtos.Response;
using my_expense_api.Models;

namespace my_expense_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Category, SimpleCategoryDTO>();
            CreateMap<CategoryInputDTO, Category>();
            CreateMap<Expense, ExpenseDTO>();
            CreateMap<ExpenseInputDTO, Expense>();
        }        
    }
}