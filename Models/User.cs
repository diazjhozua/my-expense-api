using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string FirstName {get; set;}

        public string LastName {get; set;}
        
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}