using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Models
{
    public enum ExpenseType
    {
        [Description("Credit Card")]
        CreditCard = 1,
        [Description("Debit Card")]
        DebitCard = 2,
        [Description("Cheque")]
        Cheque = 3,  
        [Description("Cash")]
        Cash = 4,              
    }
}