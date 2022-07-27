using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Components.Validators
{
    public class EmailAddressAttribute : RegularExpressionAttribute
    {
        public EmailAddressAttribute()
           : base(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                    + "@"
                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$") => ErrorMessage = "The Email field is not a valid e-mail address.";
    }
}