using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Dtos.Response
{
    public class CategoryDTO : BaseDTO
    {
        public string Name { get; set; }

        public float Limit { get; set; }               
    }

    public class SimpleCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}