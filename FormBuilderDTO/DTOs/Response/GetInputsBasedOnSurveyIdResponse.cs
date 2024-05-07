using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Response
{
    public class GetInputsBasedOnSurveyIdResponse
    {
        public List<InputsDto> Inputs { get; set; } = new();
    }
}
