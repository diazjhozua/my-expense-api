using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using my_expense_api.Models;

namespace my_expense_api.Dtos.Response
{
    public class ExpenseDTO : BaseDTO
    {
        public string Name { get; set; }  

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExpenseType Type { get; set; }
        public float Cost { get; set; } 
        public SimpleCategoryDTO Category {get; set;}
        public string Description { get; set; }  
    }
}