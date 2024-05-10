using FormBuilderDTO.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace FormBuilderDTO.DTOs.Control
{
    public class CreateControlRequest
    {
        [ValidateComplexType]
        public ControlsDto Control { get; set; } = new();
    }
}
