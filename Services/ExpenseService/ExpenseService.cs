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

namespace my_expense_api.Services.ExpenseService
{
   public class ExpenseService : IExpenseService
   {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<ExpenseDTO>>> GetAllAsync()
        {
            ServiceResponse<List<ExpenseDTO>> serviceResponse = new ServiceResponse<List<ExpenseDTO>>();
            List<Expense> dbExpenses = await _context.Expenses.Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = (dbExpenses.Select(c => _mapper.Map<ExpenseDTO>(c))).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<ExpenseDTO>> GetByIdAsync(int id)
        {
            ServiceResponse<ExpenseDTO> serviceResponse = new ServiceResponse<ExpenseDTO>();
            Expense dbExpense = await _context.Expenses.FirstOrDefaultAsync(c=> c.Id == id && c.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<ExpenseDTO>(dbExpense);
            return serviceResponse;
        }

        public async Task<ServiceResponse<ExpenseDTO>> AddAsync(ExpenseInputDTO expenseInput)
        {
            ServiceResponse<ExpenseDTO> serviceResponse = new ServiceResponse<ExpenseDTO>();
            Expense newExpense = _mapper.Map<Expense>(expenseInput);
            newExpense.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            await _context.Expenses.AddAsync(newExpense);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<ExpenseDTO>(newExpense);
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
            }
            return serviceResponse;
        }
    }
}