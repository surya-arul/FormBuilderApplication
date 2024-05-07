using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Control
{
    public class UpdateControlRequest
    {
        public ControlsDto Control { get; set; } = new();
    }
}
