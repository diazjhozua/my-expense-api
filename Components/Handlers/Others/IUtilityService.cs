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

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        //         private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        // {
        //     using(var hmac = new System.Security.Cryptography.HMACSHA512())
        //     {
        //         passwordSalt = hmac.Key;
        //         passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //     }
        // }
    }
}