using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Tech_Mart.Models
{
    public class Product : BaseEntity
    {
        [Required]
        [Range(0, 1000000)]
        public double Price { get; set; }

        [Required]
        [RegularExpression(@"^.*\.(jpg|jpeg|png|gif)$", ErrorMessage = "Only image files are allowed.")]
        public string Img { get; set; }

        [Required]
        [Range(0, 100)]
        public int Quntity { get; set; }

        [Required]
        [Range(0, 5)]
        public double Rate { get; set; }

        [Required]
        [Range(0, 100)]
        public double Discount { get; set; }


        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}
