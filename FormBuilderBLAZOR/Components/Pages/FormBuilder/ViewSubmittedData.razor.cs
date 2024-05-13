using FormBuilderDTO.DTOs.UserSubmitDetails;
using FormBuilderSharedService.Repositories;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.FormBuilder
{
    public partial class ViewSubmittedData : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private IUserDataRepository UserDataRepository { get; set; } = default!;

        private GetDataSubmittedByUserResponse _getDataSubmittedByUserResponse = new();

        protected async override Task OnInitializedAsync()
        {
            _getDataSubmittedByUserResponse = await UserDataRepository.GetDataSubmittedByUserBasedOnUserSubmitDetails(new GetDataSubmittedByUserRequest { UserSubmitDetailsId = Id });
        }
    }
}
