using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace expressForm.Web.Models
{
    public class AnswerAttribute : ValidationAttribute
    {
        public AnswerAttribute()
        {
        }
        public string GetErrorMessage() =>
        $"The Answer cannot be empty.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var answer = (AnswerViewModel) validationContext.ObjectInstance;
            var isRequired = answer.Question.IsRequired;
            if (isRequired && string.IsNullOrWhiteSpace(answer.Text))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
    }
}
