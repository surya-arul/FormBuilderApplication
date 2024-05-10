using FormBuilderDTO.DTOs.Base;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Input
{
    public partial class InputsTable : ComponentBase
    {
        [Parameter]
        public List<InputsDto> Inputs { get; set; } = new List<InputsDto>();

        [Parameter]
        public List<KeyValuePair<string, string>> ControlsList { get; set; } = [];

        [Parameter]
        public EventCallback<InputsDto> AddInput { get; set; }

        [Parameter]
        public EventCallback<InputsDto> RemoveInput { get; set; }

        private async Task AddNewInput()
        {
            await AddInput.InvokeAsync(new InputsDto());
        }
    }
}
