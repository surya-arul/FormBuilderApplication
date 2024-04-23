using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.DTOs.Request
{
    public class CreateInputRequest
    {
        public InputsDto Input { get; set; } = new();
    }
}
