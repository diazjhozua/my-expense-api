using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using my_expense_api.Data;
using my_expense_api.Models;

namespace my_expense_api.Services.AnalyticsService
{
   public class AnalyticsService : IAnalyticsService
   {

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
      
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public AnalyticsService( DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<dynamic>> getAverageExpense()
        {
            ServiceResponse<dynamic> serviceResponse = new ServiceResponse<dynamic>();
            var expenses =  await _context.Expenses.Where(c => c.UserId == GetUserId()).ToListAsync();
        
            serviceResponse.Data = await _context.Expenses.Where(c => c.UserId == 2)
                .GroupBy(x => 
                    new { 
                    x.Date.Month,       
                    })
                .Select(s => new
                {
                    month =  (new DateTime(2022, s.Key.Month, 1)).ToString("MMMM"),
                    total = s.Average(c => c.Cost)
                }).ToListAsync();

            return serviceResponse;
        }

        public Task<ServiceResponse<dynamic>> getExpenseCategoryLimitRatioThisMonth()
        {
            throw new NotImplementedException();
        }
   }
}