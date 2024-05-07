using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Input
{
    public class GetInputResponse
    {
        public InputsDto Input { get; set; } = new();
    }
}
