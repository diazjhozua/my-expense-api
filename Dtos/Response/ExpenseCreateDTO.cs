using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Dtos.Response
{
    public class ExpenseCreateDTO
    {
        public List<CategoryDTO> Categories { get; set; }
        public Dictionary<string, int> types { get; set; }
    }
}