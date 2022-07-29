using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using my_expense_api.Models;

namespace my_expense_api.Dtos.Request
{
    public class ExpenseInputDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }    

        [Required]
        [EnumDataType(typeof(ExpenseType))]
        [Display(Name = "Disease Type")]
        public ExpenseType Type { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }   

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Range(1, float.MaxValue)]
        public float Limit { get; set; }              
    }
}