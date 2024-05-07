using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.DTOs.Request
{
    public class CreateControlRequest
    {
        public ControlsDto Control { get; set; } = new();
    }
}
