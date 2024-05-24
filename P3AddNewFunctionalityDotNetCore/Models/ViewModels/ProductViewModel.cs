using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "MissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required(ErrorMessage = "MissingQuantity")]
        [Range(1, int.MaxValue, ErrorMessage = "QuantityNotGreaterThanZero")]
        public string Stock { get; set; }

        [Required(ErrorMessage = "MissingPrice")]
        [IsNumber(ErrorMessage = "PriceNotANumber")]
        [Range(0.01, double.MaxValue, ErrorMessage = "PriceNotGreaterThanZero")]
        public string Price { get; set; }
    }

    public class IsNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string strValue && !double.TryParse(strValue, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
            {
                return new ValidationResult("PriceNotANumber");
            }

            return ValidationResult.Success;
        }
    }

}
