using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_expense_api.Models;

namespace my_expense_api.Services.AnalyticsService
{
    public interface IAnalyticsService
    {
        Task<ServiceResponse<dynamic>> getAverageExpense();
        
        Task<ServiceResponse<dynamic>> getBudgetLimitThisMonth();

        Task<ServiceResponse<dynamic>> getExpenseCategorySummaryThisMonth();
    }
}