using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Input
{
    public class CreateInputRequest
    {
        public InputsDto Input { get; set; } = new();
    }
}
