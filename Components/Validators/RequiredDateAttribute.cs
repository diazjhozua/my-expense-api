using System;
using System.ComponentModel.DataAnnotations;


namespace my_expense_api.Components.Validators
{
    public class RequiredDateAttribute : RangeAttribute
    {
        public RequiredDateAttribute() : base(typeof(DateTime), DateTime.MinValue.AddDays(1).ToShortDateString(), DateTime.MaxValue.ToShortDateString()) { }
    }
}