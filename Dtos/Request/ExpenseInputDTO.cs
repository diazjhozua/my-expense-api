using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using my_expense_api.Components.Validators;
using my_expense_api.Models;

namespace my_expense_api.Dtos.Request
{
    public class ExpenseInputDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }    
        
        [Required]
        [Range(1, float.MaxValue)]
        public float Cost { get; set; }  

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Display(Name = "Disease Type")]
        public ExpenseType Type { get; set; }

        [StringLength(250)]
        public string Description { get; set; }   

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [RequiredDate]
        public DateTime Date { get; set; }     
    }
}