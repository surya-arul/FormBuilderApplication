using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Response
{
    public class GetSurveyResponse
    {
        public SurveysDto Survey { get; set; } = new();
        public List<InputsDto> Inputs { get; set; } = new();
    }
}
