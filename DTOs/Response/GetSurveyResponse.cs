using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.DTOs.Response
{
    public class GetSurveyResponse
    {
        public SurveysDto Survey { get; set; } = new();
        public List<InputsDto> Inputs { get; set; } = new();
    }
}
