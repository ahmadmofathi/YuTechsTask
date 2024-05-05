using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.BL
{
    public class NewsAddDTO
    {
        public string? Title { get; set; }
        public string? NewsContent { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Publication date is required")]
        [FutureDate(ErrorMessage = "Publication date must be between today and a week from today")]

        public string? PublicationDate { get; set; }
        public string? CreationDate { get; set; }
        public string? AuthorId { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string dateString)
            {
                if (DateTime.TryParse(dateString, out DateTime date))
                {
                    var minDate = DateTime.Today;
                    var maxDate = DateTime.Today.AddDays(7);
                    if (date >= minDate && date <= maxDate)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
                else
                {
                    return new ValidationResult("Invalid date format");
                }
            }
            else
            {
                return new ValidationResult("Invalid date value");
            }
        }
    }
}