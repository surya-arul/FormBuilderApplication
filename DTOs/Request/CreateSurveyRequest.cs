using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.DTOs.Request
{
    public class CreateSurveyRequest
    {
        public SurveysDto Survey { get; set; } = new();
    }
}
