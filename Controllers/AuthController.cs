using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_expense_api.Data;

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
    }
}