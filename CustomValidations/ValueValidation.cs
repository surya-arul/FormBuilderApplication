using FormBuilderMVC.DTOs.Base;
using FormBuilderMVC.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FormBuilderMVC.CustomValidations
{
    public class ValueValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var inputsDto = (InputsDto)validationContext.ObjectInstance;

            var validationResults = new List<ValidationResult>();

            if (Enum.TryParse(inputsDto.InputType, true, out HtmlType inputType))
            {
                var allowedInputTypes = new List<HtmlType> {
                    HtmlType.Text,
                    HtmlType.Number,
                    HtmlType.Date,
                    HtmlType.Email,
                    HtmlType.Textarea,
                    HtmlType.Button,
                    HtmlType.SubmitButton,
                };

                if (inputType == HtmlType.Number)
                {
                    if (!string.IsNullOrWhiteSpace(inputsDto.Value))
                    {
                        if (!double.TryParse(inputsDto.Value, out _))
                        {
                            // Add a validation error if parsing fails
                            validationResults.Add(new ValidationResult("Value should be a valid number."));
                        }
                    }
                }

                if (inputType == HtmlType.Date)
                {
                    if (!string.IsNullOrWhiteSpace(inputsDto.Value))
                    {
                        string[] dateFormats = { "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy", "dd/MM/yyyy" };

                        if (!DateTime.TryParseExact(inputsDto.Value, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                        {
                            validationResults.Add(new ValidationResult("Value should be a valid date."));
                        }
                    }
                }

                // Perform validation for input types not included in the allowed list
                if (!allowedInputTypes.Contains(inputType))
                {
                    if (!string.IsNullOrWhiteSpace(inputsDto.Value))
                    {
                        var allowedTypesMessage = string.Join(", ", allowedInputTypes);
                        var errorMessage = $"Value should be empty or null for input types other than {allowedTypesMessage}.";
                        validationResults.Add(new ValidationResult(errorMessage));
                    }
                }
            }
            else
            {
                validationResults.Add(new ValidationResult($"Invalid InputType: {inputsDto.InputType}"));
            }

            return validationResults.Count is not 0 ? new ValidationResult(string.Join(", ", validationResults)) : ValidationResult.Success;
        }
    }
}
