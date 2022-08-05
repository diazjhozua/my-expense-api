using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace my_expense_api.Controllers
{
    [ApiController]
    [Route("[controller]")]   
    public class WelcomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hi Welcome user! This is a my expense api app created by Jhozua Diaz. To use this application, go to this specified link: ");
        }           
    }
}