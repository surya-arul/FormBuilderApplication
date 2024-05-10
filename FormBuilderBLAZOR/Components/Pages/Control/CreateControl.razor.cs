using FormBuilderDTO.DTOs.Control;
using FormBuilderSharedService.Repositories;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Control
{
    public partial class CreateControl : ComponentBase
    {
        private CreateControlRequest _createControlRequest { get; set; } = new();

        protected async override Task OnInitializedAsync()
        {
            _createControlRequest.Control.OptionData = ["one","two"];
        }

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
            _createControlRequest.Control.OptionData?.Add("");

        }

        private async Task RemoveOption(string options)
        {
            _createControlRequest.Control.OptionData?.Remove(options);
            await InvokeAsync(StateHasChanged);
        }
    }
}