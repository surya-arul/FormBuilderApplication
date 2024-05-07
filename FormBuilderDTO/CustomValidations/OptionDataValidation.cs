using FormBuilderDTO.Constants;
using FormBuilderDTO.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace FormBuilderDTO.CustomValidations
{
    public class OptionDataValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var controlsDto = (ControlsDto)validationContext.ObjectInstance;

            var validationResults = new List<ValidationResult>();

            if (Enum.TryParse(controlsDto.InputType, true, out HtmlType inputType))
            {
                var allowedInputTypes = new List<HtmlType> {
                    HtmlType.CheckBox,
                    HtmlType.RadioButton,
                    HtmlType.Select
                };

                // Perform validation for input types not included in the allowed list
                if (!allowedInputTypes.Contains(inputType))
                {
                    if (controlsDto.OptionData is not null && controlsDto?.OptionData?.Count > 0)
                    {
                        var allowedTypesMessage = string.Join(", ", allowedInputTypes);
                        var errorMessage = $"Option Data should be empty for input types other than {allowedTypesMessage}.";
                        validationResults.Add(new ValidationResult(errorMessage));
                    }
                }
            }
            else
            {
                validationResults.Add(new ValidationResult($"Invalid InputType: {controlsDto.InputType}"));
            }

            return validationResults.Count is not 0 ? new ValidationResult(string.Join(", ", validationResults)) : ValidationResult.Success;
        }
    }
}
