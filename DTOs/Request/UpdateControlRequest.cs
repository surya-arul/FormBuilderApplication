using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.DTOs.Request
{
    public class UpdateControlRequest
    {
        public ControlsDto Control { get; set; } = new();
    }
}
