using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Request
{
    public class UpdateSurveyRequest
    {
        public SurveysDto Survey { get; set; } = new();
    }
}
