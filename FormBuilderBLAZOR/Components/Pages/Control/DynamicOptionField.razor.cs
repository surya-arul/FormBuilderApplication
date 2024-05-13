using FormBuilderDTO.DTOs.Base;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Control
{
    public partial class DynamicOptionField : ComponentBase
    {
        [Parameter]
        public ControlsDto Control { get; set; } = new();

        [Parameter]
        public string Name { get; set; } = string.Empty;

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public EventCallback<string> OnRemove { get; set; }

        private async Task RemoveOption()
        {
            await OnRemove.InvokeAsync(Control.OptionData?[Index]);
            Index = 0;
        }
    }
}
