using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Components.Handlers.Others
{
   public class UtilityService : IUtilityService
   {
        public dynamic FormatObjectResult(int code, dynamic message = null, object data = null)
        {
            dynamic[] messages = { null };
            IDictionary<string, object> result = new ExpandoObject();
            result.Add("status", code);
            var title = "";
            switch (code)
            {
                case 400:
                    title = "One or more validation errors detected.";
                    messages[0] = message;
                    break;
                case 404:
                    title = "Record does not exist.";
                    message += " does not exist.";
                    break;
                case 409:
                    title = "Record already exists.";
                    break;
            }
            if (!string.IsNullOrEmpty(title)) result.Add("title", title);
            if (messages[0] != null) result.Add("message", messages);
            else result.Add("message", message);
            if (data != null) result.Add("data", data);

            // _log.AddAsync(new Log
            // {
            //     Message = message,
            //     Code = code
            // });
            return result;
        }

        public string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }

   }
}