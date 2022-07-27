using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_expense_api.Components.Handlers.Others;

namespace my_expense_api.Components.Handlers
{
    public class Handler : IHandler
    {
        public IUtilityService Utility { get; }

        public Handler(
            IUtilityService utility) 
        { 
            Utility = utility;
        }
    }
}