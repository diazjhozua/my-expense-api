using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Dtos.Request
{
    public class LoginDTO
    {
        [Required]
        [StringLength(100)]
        [Components.Validators.EmailAddressAttribute]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }       
    }
}