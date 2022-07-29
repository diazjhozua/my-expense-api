using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Models
{
    public class Category : BaseEntity
    {
        public Category()
        {
            DateCreated= DateTime.Now;
        }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        public float Limit { get; set; }

        public DateTime DateCreated { get; set; } 
        
        public DateTime? DateModified { get; set; }

        public User User { get; set; }
    }
}