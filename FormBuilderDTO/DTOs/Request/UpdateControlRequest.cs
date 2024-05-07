using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Request
{
    public class UpdateControlRequest
    {
        public ControlsDto Control { get; set; } = new();
    }
}
