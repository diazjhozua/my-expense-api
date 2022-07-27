using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Components.Handlers.Others
{
    public interface IUtilityService
    {
        
        dynamic FormatObjectResult(int code, dynamic message = null, object data = null);

        public string SplitCamelCase(string input);
    }
}