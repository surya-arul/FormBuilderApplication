using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Request
{
    public class UpdateInputRequest
    {
        public InputsDto Input { get; set; } = new();
    }
}
