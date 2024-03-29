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
using static my_expense_api.Configuration.AppSettings;

namespace my_expense_api.Services.ExpenseService
{
   public class ExpenseService : IExpenseService
   {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public ExpenseService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<List<ExpenseDTO>>> GetAllAsync()
        {
            ServiceResponse<List<ExpenseDTO>> serviceResponse = new ServiceResponse<List<ExpenseDTO>>();
            List<Expense> dbExpenses = await _context.Expenses.Include(x=> x.Category).Where(c => c.User.Id == GetUserId()).OrderByDescending(c=> c.Date).ToListAsync();
            serviceResponse.Data = (dbExpenses.Select(c => _mapper.Map<ExpenseDTO>(c))).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<ExpenseDTO>> GetByIdAsync(int id)
        {
            ServiceResponse<ExpenseDTO> serviceResponse = new ServiceResponse<ExpenseDTO>();
            Expense dbExpense = await _context.Expenses.Include(x=> x.Category).FirstOrDefaultAsync(c=> c.Id == id && c.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<ExpenseDTO>(dbExpense);
            return serviceResponse;
        }

        public async Task<ServiceResponse<ExpenseDTO>> AddAsync(ExpenseInputDTO expenseInput)
        {
            ServiceResponse<ExpenseDTO> serviceResponse = new ServiceResponse<ExpenseDTO>();
            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(c=> c.Id == expenseInput.CategoryId &&  c.User.Id == GetUserId());
            if (dbCategory != null) 
            {
                Expense newExpense = _mapper.Map<Expense>(expenseInput);
                newExpense.Category = dbCategory;
                newExpense.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
                await _context.Expenses.AddAsync(newExpense);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<ExpenseDTO>(newExpense);
                serviceResponse.Message = "New " + Entities.Expense + " has been created successfully"; 
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ExpenseDTO>> UpdateAsync(int id, ExpenseInputDTO expenseInput)
        {
            ServiceResponse<ExpenseDTO> serviceResponse = new ServiceResponse<ExpenseDTO>();

            Expense dbExpense = await _context.Expenses.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            if (dbExpense != null) {
                Category dbCategory = await _context.Categories.FirstOrDefaultAsync(c=> c.Id == expenseInput.CategoryId &&  c.User.Id == GetUserId());
                if (dbCategory != null) 
                {
                    dbExpense.Name = expenseInput.Name;
                    dbExpense.Cost = expenseInput.Cost;
                    dbExpense.Category = dbCategory;
                    dbExpense.Type = expenseInput.Type;
                    if (expenseInput.Description != null) dbExpense.Description = expenseInput.Description;                
                    dbExpense.DateModified = DateTime.Now;

                    _context.Expenses.Update(dbExpense);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<ExpenseDTO>(dbExpense);
                    serviceResponse.Message = Entities.Expense + " has been updated successfully"; 
                }

            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

            Expense dbExpense = await _context.Expenses.FirstOrDefaultAsync(c => c.User.Id == GetUserId() && c.Id == id);
            if(dbExpense != null)
            {
                _context.Expenses.Remove(dbExpense);
                await _context.SaveChangesAsync();
                serviceResponse.Data = true;
                serviceResponse.Message = Entities.Expense + " has been deleted successfully"; 
            }
            return serviceResponse;
        }
    }
}