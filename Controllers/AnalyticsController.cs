using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using my_expense_api.Components.Handlers;
using my_expense_api.Models;
using my_expense_api.Services.AnalyticsService;

namespace my_expense_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]    
    public class AnalyticsController : BaseController
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService, IHandler handler): base(handler)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(
                new 
                { 
                    averageExpense = (await _analyticsService.getAverageExpense()).Data, 
                    budgetLimitThisMonth = (await _analyticsService.getBudgetLimitThisMonth()).Data,
                    expenseCategorySummaryThisMonth = (await _analyticsService.getExpenseCategorySummaryThisMonth()).Data
                }
            );
        }

        [HttpGet("averageExpense")]
        public async Task<IActionResult> GetAverageExpense()
        {
            return Ok((await _analyticsService.getAverageExpense()).Data);
        } 

        [HttpGet("budgetLimitThisMonth")]
        public async Task<IActionResult> GetBudgetLimitThisMonth()
        {
            return Ok((await _analyticsService.getBudgetLimitThisMonth()).Data);
        }

        [HttpGet("expenseCategorySummaryThisMonth")]
        public async Task<IActionResult> GetExpenseCategorySummaryThisMonth()
        {
            return Ok((await _analyticsService.getExpenseCategorySummaryThisMonth()).Data);
        }                
    }
}