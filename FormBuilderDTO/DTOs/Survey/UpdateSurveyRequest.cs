using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Survey
{
    public class UpdateSurveyRequest
    {
        public SurveysDto Survey { get; set; } = new();
    }
}
