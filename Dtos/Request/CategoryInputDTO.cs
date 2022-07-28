using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_expense_api.Dtos.Request
{
    public class CategoryInputDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }     

        [Required]
        [Range(1, float.MaxValue)]
        public float Limit { get; set; }           
    }
}