using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using my_expense_api.Components.Handlers;
using my_expense_api.Dtos.Request;
using my_expense_api.Dtos.Response;
using my_expense_api.Models;
using my_expense_api.Services.CategoryService;
using static my_expense_api.Configuration.AppSettings;

namespace my_expense_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]    
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, IHandler handler): base(handler)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryService.GetAllAsync());
        }   

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResponse<CategoryDTO> serviceResponse = await _categoryService.GetByIdAsync(id);
            if (serviceResponse.Data == null) return new NotFoundObjectResult(_handler.Utility.FormatObjectResult(404, Entities.Category, new { id }));
            return Ok(serviceResponse);
        }  

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryInputDTO categoryInput)
        {
            return Ok(await _categoryService.AddAsync(categoryInput));
        }   
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryInputDTO categoryInput)
        {
            ServiceResponse<CategoryDTO> serviceResponse = await _categoryService.UpdateAsync(id, categoryInput);
            if (serviceResponse.Data == null) return new NotFoundObjectResult(_handler.Utility.FormatObjectResult(404, Entities.Category, new { id }));
            return Ok(serviceResponse);
        }     


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<bool> serviceResponse = await _categoryService.DeleteAsync(id);
            if (serviceResponse.Data != true) return new NotFoundObjectResult(_handler.Utility.FormatObjectResult(404, Entities.Category, new { id }));
            return Ok(serviceResponse);
        }

    }
}