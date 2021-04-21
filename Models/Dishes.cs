using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,6)]
        public int Tastiness { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage="You need to run to get negative calories!")]
        public int Calories { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ChefId{get;set;}
        public Chef Creator{get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}