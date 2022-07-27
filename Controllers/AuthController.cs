using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_expense_api.Data;
using my_expense_api.Dtos.Request;
using my_expense_api.Models;

namespace my_expense_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;  

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        } 

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO request) {
            ServiceResponse<int> response = await _authRepo.Register(
                new User { Email = request.Email}, request.Password
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
    }
}