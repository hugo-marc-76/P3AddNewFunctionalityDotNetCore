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
        [Required(ErrorMessage = "MissingStock")]
        [Range(1, int.MaxValue, ErrorMessage = "StockNotGreaterThanZero")]
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
            // Si la valeur est une chaîne, on essaie de la convertir en nombre (en utilisant le format invariant)
            if (value is string strValue && !double.TryParse(strValue, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
            {
                // La méthode FormatErrorMessage() va récupérer le message d'erreur localisé grâce aux propriétés ErrorMessageResourceType et ErrorMessageResourceName
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}
