using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Response
{
    public class GetInputResponse
    {
        public InputsDto Input { get; set; } = new();
    }
}
