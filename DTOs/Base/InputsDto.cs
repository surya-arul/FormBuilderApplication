using System.ComponentModel.DataAnnotations;

namespace FormBuilderMVC.DTOs.Base
{
    public class InputsDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Survey id")]
        public int SurveyId { get; set; }

        [Display(Name = "Order no")]
        public int OrderNo { get; set; }

        [Display(Name = "Input type")]
        public string InputType { get; set; } = null!;

        [Display(Name = "Internal name")]
        public string InternalName { get; set; } = null!;

        [Display(Name = "Div class name")]
        public string? DivClassName { get; set; }

        [Display(Name = "Input class name")]
        public string? InputClassName { get; set; }

        [Display(Name = "Label")]
        public string? Label { get; set; }

        [Display(Name = "Hide label")]
        public bool ShouldHideLabel { get; set; }

        [Display(Name = "Label class name")]
        public string? LabelClassName { get; set; }

        [Display(Name = "Value")]
        public string? Value { get; set; }

        [Display(Name = "Autofocus")]
        public bool IsAutofocus { get; set; }

        [Display(Name = "Placeholder")]
        public string? Placeholder { get; set; } // Only applies to text and textarea

        [Display(Name = "Required")]
        public bool IsRequired { get; set; } // Only applies to text and textarea

        [Display(Name = "Option data")]
        public List<string>? OptionData { get; set; } // Only applies to select,checkbox and radio fields
    }
}
