using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Control;
using FormBuilderSharedService.Repositories;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Control
{
    public partial class CreateControl : ComponentBase
    {
        private CreateControlRequest _createControlRequest { get; set; } = new CreateControlRequest
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

        private async Task SubmitControl()
        {
            await ControlRepository.CreateControl(_createControlRequest);
            NavigationManager.NavigateTo("/Surveys");
        }

        private void AddOption()
        {
            _createControlRequest.Control.OptionData?.Add(string.Empty);
        }

        private async Task RemoveOption(string option)
        {
            _createControlRequest.Control.OptionData?.Remove(option);
            await InvokeAsync(StateHasChanged);
        }
    }
}