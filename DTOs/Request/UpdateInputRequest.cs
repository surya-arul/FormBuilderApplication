using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.DTOs.Request
{
    public class UpdateInputRequest
    {
        public InputsDto Input { get; set; } = new();
    }
}
