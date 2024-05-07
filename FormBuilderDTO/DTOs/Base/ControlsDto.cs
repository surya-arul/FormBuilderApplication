using FormBuilderDTO.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace FormBuilderDTO.DTOs.Base
{
    public class ControlsDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Internal name")]
        [Required]
        public string InternalName { get; set; } = null!;

        [Display(Name = "Input type")]
        [Required]
        public string InputType { get; set; } = null!;

        [Display(Name = "Div class name")]
        public string? DivClassName { get; set; }

        [Display(Name = "Input class name")]
        public string? InputClassName { get; set; }

        [Display(Name = "Label")]
        [LabelValidation]
        public string? Label { get; set; }

        [Display(Name = "Hide label")]
        public bool ShouldHideLabel { get; set; }

        [Display(Name = "Label class name")]
        public string? LabelClassName { get; set; }

        [Display(Name = "Value")]
        [ValueValidation]
        public string? Value { get; set; }

        [Display(Name = "Autofocus")]
        public bool IsAutofocus { get; set; }

        [Display(Name = "Placeholder")]
        [PlaceholderValidation]
        public string? Placeholder { get; set; }

        [Display(Name = "Required")]
        [RequiredValidation]
        public bool IsRequired { get; set; }

        [Display(Name = "Option data")]
        [OptionDataValidation]
        public List<string>? OptionData { get; set; }
    }
}
