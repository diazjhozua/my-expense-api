using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using my_expense_api.Components.Handlers;
using my_expense_api.Data;
using my_expense_api.Dtos.Request;
using my_expense_api.Dtos.Response;
using my_expense_api.Models;
using static my_expense_api.Configuration.AppSettings;

namespace my_expense_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthRepository _authRepo;  

        public AuthController(IAuthRepository authRepo, IHandler handler): base(handler)
        {
            _authRepo = authRepo;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO request) {
            ServiceResponse<int> response = await _authRepo.Register(
                new User { Email = request.Email, FirstName = request.FirstName, LastName = request.LastName}, request.Password
            );

            if(!response.Success)
            {
                return BadRequest(response);
            } 
            
            return Ok(response);
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO request) {
            ServiceResponse<string> response = await _authRepo.Login(
                request.Email, request.Password
            );

            if(!response.Success)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }

        [HttpGet("Me")]
        [Authorize]
        public async Task<IActionResult> Get() {
            ServiceResponse<UserDTO> serviceResponse = await _authRepo.Me();
            if (serviceResponse.Data == null) return new NotFoundObjectResult(_handler.Utility.FormatObjectResult(404, Entities.Category, new { }));
            return Ok(serviceResponse);
        }

    }
}