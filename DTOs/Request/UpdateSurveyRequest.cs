using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.DTOs.Request
{
    public class UpdateSurveyRequest
    {
        public SurveysDto Survey { get; set; } = new();
    }
}
