using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Dtos.Response
{
    public class UserDTO
    {
        public string Email { get; set; }

        public string FirstName {get; set; }

        public string LastName {get; set; }
        
    }
}