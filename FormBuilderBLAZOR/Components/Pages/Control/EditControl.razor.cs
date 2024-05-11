using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Control;
using FormBuilderSharedService.Repositories;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Control
{
    public partial class EditControl : ComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            var existingControl = await ControlRepository.GetControlById(new GetControlRequest { Id = Id });

            UpdateControlRequest = new UpdateControlRequest
            { 
                Control = existingControl.Control 
            };
        }

        [Parameter]
        public int Id { get; set; }
        private UpdateControlRequest UpdateControlRequest { get; set; } = new UpdateControlRequest
        {
            Control = new ControlsDto
            {
                OptionData = []
            }
        };

        [Inject]
        private IControlRepository ControlRepository { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private async Task UpdateControl()
        {
            await ControlRepository.UpdateControl(UpdateControlRequest);
            NavigationManager.NavigateTo("/Controls");
        }

        private void AddOption()
        {
            UpdateControlRequest.Control.OptionData?.Add(string.Empty);
        }

        private async Task RemoveOption(string option)
        {
            UpdateControlRequest.Control.OptionData?.Remove(option);
            await InvokeAsync(StateHasChanged);
        }
    }
}
