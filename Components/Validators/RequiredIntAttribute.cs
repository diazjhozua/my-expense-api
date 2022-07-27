using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Components.Validators
{
    public class RequiredIntAttribute : RangeAttribute
    {
        public RequiredIntAttribute() : base(1, int.MaxValue) { }
    }
}