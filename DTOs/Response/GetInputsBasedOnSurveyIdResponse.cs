using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.DTOs.Response
{
    public class GetInputsBasedOnSurveyIdResponse
    {
        public List<InputsDto> Inputs { get; set; } = new();
    }
}
