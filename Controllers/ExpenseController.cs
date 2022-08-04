using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_expense_api.Components.Handlers;
using my_expense_api.Dtos.Request;
using my_expense_api.Dtos.Response;
using my_expense_api.Models;
using my_expense_api.Services.CategoryService;
using my_expense_api.Services.ExpenseService;
using static my_expense_api.Configuration.AppSettings;

namespace my_expense_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]  
    public class ExpenseController : BaseController
    {
        private readonly IExpenseService _expenseService;
        private readonly ICategoryService _categoryService;


        public ExpenseController(IExpenseService expenseService, ICategoryService categoryService, IHandler handler): base(handler)
        {
            _expenseService = expenseService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _expenseService.GetAllAsync());
        }   

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResponse<ExpenseDTO> serviceResponse = await _expenseService.GetByIdAsync(id);
            if (serviceResponse.Data == null) return new NotFoundObjectResult(_handler.Utility.FormatObjectResult(404, Entities.Expense, new { id }));
            return Ok(serviceResponse);
        }  

        [HttpGet("create")]
        public async Task<IActionResult> GetCreate()
        {
            ServiceResponse<ExpenseCreateDTO> serviceResponse = new ServiceResponse<ExpenseCreateDTO>();
            ExpenseCreateDTO expenseCreateDTO = new ExpenseCreateDTO();

            expenseCreateDTO.Categories =  (await _categoryService.GetAllAsync()).Data;
            expenseCreateDTO.types =  ((ExpenseType[])Enum.GetValues(typeof(ExpenseType))).ToDictionary(k => _handler.Utility.SplitCamelCase(k.ToString()), v => (int)v);

            serviceResponse.Data = expenseCreateDTO;

            return Ok(serviceResponse);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExpenseInputDTO expenseInput)
        {
            ServiceResponse<ExpenseDTO> serviceResponse = await _expenseService.AddAsync(expenseInput);
            if (serviceResponse.Data == null) return new NotFoundObjectResult(_handler.Utility.FormatObjectResult(404, Entities.Category, new { expenseInput.CategoryId }));
            return Ok(serviceResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ExpenseInputDTO expenseInput)
        {
            ServiceResponse<ExpenseDTO> serviceResponse = await _expenseService.UpdateAsync(id, expenseInput);
            if (serviceResponse.Data == null) return new NotFoundObjectResult(_handler.Utility.FormatObjectResult(404, Entities.Expense, new { id }));
  
            return Ok(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<bool> serviceResponse = await _expenseService.DeleteAsync(id);
            if (serviceResponse.Data != true) return new NotFoundObjectResult(_handler.Utility.FormatObjectResult(404, Entities.Expense, new { id }));
            return Ok(serviceResponse);
        }                          
    }
}