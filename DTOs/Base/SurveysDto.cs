using System.ComponentModel.DataAnnotations;

namespace FormBuilderMVC.DTOs.Base
{
    public class SurveysDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Survey title")]
        public string Title { get; set; } = null!;

        [Display(Name = "Open date")]
        public DateOnly OpenDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Display(Name = "End date")]
        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    }
}
