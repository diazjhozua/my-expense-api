using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Models
{
    public class ServiceResponse<T>
    {
         public T Data { get; set; }

        public bool Success { get; set; } = true;

        public string Messsage { get; set; } = null;
    }
}