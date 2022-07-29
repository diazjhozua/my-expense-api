using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            DateCreated= DateTime.Now;
        }        
        [Key]
        public int Id { get; set; }  
        public DateTime DateCreated { get; set; } 
        
        public DateTime? DateModified { get; set; }
  
    }
}