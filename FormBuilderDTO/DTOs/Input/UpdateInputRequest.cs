using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Input
{
    public class UpdateInputRequest
    {
        public InputsDto Input { get; set; } = new();
    }
}
