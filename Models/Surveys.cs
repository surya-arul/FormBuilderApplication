using System.ComponentModel.DataAnnotations;

namespace FormBuilderMVC.Models
{
    public class Surveys
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display (Name = "Survey title")]
        public string Title { get; set; } = null!;

        [Display(Name = "Open date")]
        public DateTime OpenDate { get; set; }

        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        public List<Inputs>? Inputs { get; set; }
    }
}
