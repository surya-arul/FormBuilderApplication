using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.DTOs.Response
{
    public class GetInputResponse
    {
        public InputsDto Input { get; set; } = new();
    }
}
