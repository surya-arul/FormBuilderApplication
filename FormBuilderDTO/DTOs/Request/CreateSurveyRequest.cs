using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Request
{
    public class CreateSurveyRequest
    {
        public SurveysDto Survey { get; set; } = new();
    }
}
