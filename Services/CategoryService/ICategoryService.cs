using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_expense_api.Dtos.Request;
using my_expense_api.Dtos.Response;
using my_expense_api.Models;

namespace my_expense_api.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<CategoryDTO>>> GetAllAsync();
        Task<ServiceResponse<CategoryDTO>> GetByIdAsync(int id);
        Task<ServiceResponse<CategoryDTO>> AddAsync(CategoryInputDTO categoryInput);
        Task<ServiceResponse<CategoryDTO>> UpdateAsync(int id, CategoryInputDTO categoryInput);
        Task<ServiceResponse<List<CategoryDTO>>> DeleteAsync(int id);                
    }
}