using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Control
{
    public class CreateControlRequest
    {
        public ControlsDto Control { get; set; } = new();
    }
}
