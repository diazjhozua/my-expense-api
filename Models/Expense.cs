using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Models
{
    public class Expense : BaseEntity
    {
        public string Name { get; set; }     
        public string Cost { get; set; } 
        public string Description { get; set; } 
        public ExpenseType Type {get; set;}
        public User User { get; set; }
        public Category Category { get; set; }   
    }
}