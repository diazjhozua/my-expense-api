using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_expense_api.Dtos.Request;
using my_expense_api.Dtos.Response;
using my_expense_api.Models;

namespace my_expense_api.Services.ExpenseService
{
    public interface IExpenseService
    {
        Task<ServiceResponse<List<ExpenseDTO>>> GetAllAsync();
        Task<ServiceResponse<ExpenseDTO>> GetByIdAsync(int id);
        Task<ServiceResponse<ExpenseDTO>> AddAsync(ExpenseInputDTO expenseInput);
        Task<ServiceResponse<ExpenseDTO>> UpdateAsync(int id, ExpenseInputDTO expenseInput);
        Task<ServiceResponse<bool>> DeleteAsync(int id);                
    }
}