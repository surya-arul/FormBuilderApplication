using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.UserSubmitDetails
{
    public class GetUserSubmitDetailsBasedOnSurveyResponse
    {
        public SurveysDto Survey { get; set; } = new();
        public List<UserSubmitDetailsDto> UserSubmitDetails { get; set; } = [];
    }
}
