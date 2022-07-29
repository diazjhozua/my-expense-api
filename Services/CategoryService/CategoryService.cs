using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using my_expense_api.Data;
using my_expense_api.Dtos.Request;
using my_expense_api.Dtos.Response;
using my_expense_api.Models;

namespace my_expense_api.Services.CategoryService
{
   public class CategoryService : ICategoryService
   {  
      private readonly IMapper _mapper;
      private readonly DataContext _context;
      private readonly IHttpContextAccessor _httpContextAccessor;
      
      private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
      
      public CategoryService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
      {
         _context = context;
         _mapper = mapper;
         _httpContextAccessor = httpContextAccessor;
      }

      public async Task<ServiceResponse<List<CategoryDTO>>> GetAllAsync()
      {
         ServiceResponse<List<CategoryDTO>> serviceResponse = new ServiceResponse<List<CategoryDTO>>();
         List<Category> dbCategories = await _context.Categories.ToListAsync();
         serviceResponse.Data = (dbCategories.Select(c => _mapper.Map<CategoryDTO>(c))).ToList();
         return serviceResponse;
      }

      public async Task<ServiceResponse<CategoryDTO>> GetByIdAsync(int id)
      {
         ServiceResponse<CategoryDTO> serviceResponse = new ServiceResponse<CategoryDTO>();
         Category dbCategory = await _context.Categories.FirstOrDefaultAsync(c=> c.Id == id);
         serviceResponse.Data = _mapper.Map<CategoryDTO>(dbCategory);
         return serviceResponse;
      }
      
      public async Task<ServiceResponse<CategoryDTO>> AddAsync(CategoryInputDTO categoryInput)
      {
         ServiceResponse<CategoryDTO> serviceResponse = new ServiceResponse<CategoryDTO>();
         Category newCategory = _mapper.Map<Category>(categoryInput);
         newCategory.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
         await _context.Categories.AddAsync(newCategory);
         await _context.SaveChangesAsync();
         serviceResponse.Data = _mapper.Map<CategoryDTO>(newCategory);
         return serviceResponse;
      }

      public async Task<ServiceResponse<CategoryDTO>> UpdateAsync(int id, CategoryInputDTO categoryInput)
      {
         ServiceResponse<CategoryDTO> serviceResponse = new ServiceResponse<CategoryDTO>();

         Category dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
         if (dbCategory != null) {
            dbCategory.Name = categoryInput.Name;
            dbCategory.Limit = categoryInput.Limit;
            dbCategory.DateModified = DateTime.Now;

            _context.Categories.Update(dbCategory);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<CategoryDTO>(dbCategory);
         }
         return serviceResponse;
      }

      public async Task<ServiceResponse<bool>> DeleteAsync(int id)
      {
         ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

         Category dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.User.Id == GetUserId() && c.Id == id);
         if(dbCategory != null)
         {
            _context.Categories.Remove(dbCategory);
            await _context.SaveChangesAsync();
            serviceResponse.Data = true;
         }
         return serviceResponse;
      }
   }
}