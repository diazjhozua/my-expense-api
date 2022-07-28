using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }  
      
    }
}