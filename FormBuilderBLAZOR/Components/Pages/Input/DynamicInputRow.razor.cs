using FormBuilderDTO.DTOs.Base;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Input
{
    public partial class DynamicInputRow : ComponentBase
    {
        [Parameter]
        public InputsDto Input { get; set; } = new();

        [Parameter]
        public List<KeyValuePair<string, string>> ControlsList { get; set; } = [];

        [Parameter]
        public EventCallback<InputsDto> OnRemove { get; set; }

        private async Task RemoveInput()
        {
            await OnRemove.InvokeAsync(Input);
        }
    }
}
