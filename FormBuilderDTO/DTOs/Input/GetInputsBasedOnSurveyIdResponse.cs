using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Input
{
    public class GetInputsBasedOnSurveyIdResponse
    {
        public List<InputsDto> Inputs { get; set; } = new();
    }
}
