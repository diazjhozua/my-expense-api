using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_expense_api.Components.Handlers;

namespace my_expense_api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IHandler _handler;

        public BaseController(IHandler handler) => _handler = handler;        
    }
}