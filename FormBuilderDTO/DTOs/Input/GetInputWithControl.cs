using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Input
{
    public class GetInputWithControl : InputsDto
    {
        public ControlsDto Control { get; set; } = new ControlsDto();
    }
}
