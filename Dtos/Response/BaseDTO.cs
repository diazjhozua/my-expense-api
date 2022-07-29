using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Dtos.Response
{
    public class BaseDTO
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; } 
        
        public DateTime? DateModified { get; set; }                
    }
}