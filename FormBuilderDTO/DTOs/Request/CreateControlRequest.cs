using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Request
{
    public class CreateControlRequest
    {
        public ControlsDto Control { get; set; } = new();
    }
}
