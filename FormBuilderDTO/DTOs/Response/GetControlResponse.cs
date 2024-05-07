using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Response
{
    public class GetControlResponse
    {
        public ControlsDto Control { get; set; } = new();

    }
}
