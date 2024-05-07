using System.ComponentModel.DataAnnotations;

namespace FormBuilderDTO.DTOs.Base
{
    public class InputsDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Survey id")]
        [Range(1, int.MaxValue, ErrorMessage = "Should not be zero.")]
        public int SurveyId { get; set; }

        [Display(Name = "Control id")]
        [Range(1, int.MaxValue, ErrorMessage = "Should not be zero.")]
        public int ControlId { get; set; }

        [Display(Name = "Order no")]
        [Range(1, int.MaxValue, ErrorMessage = "Should not be zero.")]
        public int OrderNo { get; set; }

        //public ControlsDto Control { get; set; } = new();
    }
}
