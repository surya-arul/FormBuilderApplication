using System.ComponentModel.DataAnnotations;

namespace FormBuilderDTO.DTOs.Base
{
    public class UserSubmitDetailsDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Survey Id")]
        public int SurveyId { get; set; }

        [Display(Name = "User Id")]
        public string UserId { get; set; } = null!;

        [Display(Name = "Date Created By")]
        public DateTime DateCreatedBy { get; set; }

    }
}
