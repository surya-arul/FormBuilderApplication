using FormBuilderDTO.DTOs.Base;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Control
{
    public partial class ControlCreateAndEdit : ComponentBase
    {
        [Parameter]
        public ControlsDto Control { get; set; } = new();

/*        public List<string> Control { get; set; } = new();*/

        [Parameter]
        public EventCallback<string> AddOption { get; set; }

        private async Task AddNewOption()
        {
            await AddOption.InvokeAsync();
        }

        [Parameter]
        public EventCallback<string> RemoveOption { get; set; }
    }
}
