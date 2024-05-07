using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Control
{
    public class GetAllControlsResponse
    {
        public List<ControlsDto> Controls { get; set; } = new();

    }
}
