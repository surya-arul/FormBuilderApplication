using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Control;
using FormBuilderSharedService.Repositories;
using FormBuilderSharedService.Utilities;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Control
{
    public partial class CreateControl : ComponentBase
    {
        private CreateControlRequest CreateControlRequest { get; set; } = new CreateControlRequest
        {
            Control = new ControlsDto
            {
                InputType = HtmlHelper.HtmlTypeDropdownList.FirstOrDefault().Value,
                OptionData = []
            }
        };

        [Inject]
        private IControlRepository ControlRepository { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private async Task AddControl()
        {
            await ControlRepository.CreateControl(CreateControlRequest);
            NavigationManager.NavigateTo("/Controls");
        }

        private void AddOption()
        {
            CreateControlRequest.Control.OptionData?.Add(string.Empty);
        }

        private async Task RemoveOption(string option)
        {
            CreateControlRequest.Control.OptionData?.Remove(option);
            await InvokeAsync(StateHasChanged);
        }
    }
}