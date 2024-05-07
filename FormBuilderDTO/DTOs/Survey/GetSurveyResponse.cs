using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Input;

namespace FormBuilderDTO.DTOs.Survey
{
    public class GetSurveyResponse
    {
        public SurveysDto Survey { get; set; } = new();
        public List<GetInputWithControl> Inputs { get; set; } = new();
    }
}
