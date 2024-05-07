using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Response
{
    public class GetAllControlsResponse
    {
        public List<ControlsDto> Controls { get; set; } = new();

    }
}
