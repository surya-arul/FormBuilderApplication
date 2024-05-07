using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Request
{
    public class CreateInputRequest
    {
        public InputsDto Input { get; set; } = new();
    }
}
