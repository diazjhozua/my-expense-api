using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_expense_api.Components.Handlers.Others;

namespace my_expense_api.Components.Handlers
{
    public interface IHandler
    {
        IUtilityService Utility { get; }
    }
}