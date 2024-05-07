using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.DTOs.Response
{
    public class GetAllControlsResponse
    {
        public List<ControlsDto> Controls { get; set; } = new();

    }
}
