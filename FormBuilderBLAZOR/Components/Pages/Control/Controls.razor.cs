using FormBuilderDTO.DTOs.Control;
using FormBuilderSharedService.Repositories;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Control
{
    public partial class Controls : ComponentBase
    {
        private GetAllControlsResponse _getAllControlsResponse = new();

        [Inject]
        private IControlRepository ControlRepository { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            _getAllControlsResponse = await GetAllControls();
        }

        private async Task<GetAllControlsResponse> GetAllControls()
        {
            return await ControlRepository.GetAllControls();
        }

        private void NavigateToCreateControl()
        {
            NavigationManager.NavigateTo($"/{nameof(CreateControl)}");

        }

        private void EditControl(int id)
        {
            NavigationManager.NavigateTo($"/{nameof(FormBuilderBLAZOR.Components.Pages.Control.EditControl)}/{id}");

        }

        private async Task DeleteControl(int id)
        {
            var response = await ControlRepository.DeleteControl(new DeleteControlRequest { Id = id });

            // Refresh list
            _getAllControlsResponse = await GetAllControls();
        }
    }
}
