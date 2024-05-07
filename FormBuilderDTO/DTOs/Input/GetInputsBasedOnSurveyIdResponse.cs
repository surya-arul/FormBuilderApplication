using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Input
{
    public class GetInputsBasedOnSurveyIdResponse
    {
        public SurveysDto Survey { get; set; } = new();
        public List<GetInputWithControl> Inputs { get; set; } = new();
    }
}
