using FormBuilderDTO.DTOs.UserSubmitDetails;
using FormBuilderSharedService.Repositories;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.FormBuilder
{
    public partial class UserSubmitDetailsList : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private IUserSubmitDetailsRepository UserSubmitDetailsRepository { get; set; } = default!;

        private GetUserSubmitDetailsBasedOnSurveyResponse _getUserSubmitDetailsBasedOnSurveyResponse = new();

        protected async override Task OnInitializedAsync()
        {
            _getUserSubmitDetailsBasedOnSurveyResponse = await UserSubmitDetailsRepository.GetUserSubmitDetailsBasedOnSurvey(new GetUserSubmitDetailsBasedOnSurveyRequest { SurveyId = Id });
        }

        private void ViewDataSubmittedByUser(int id)
        {
            NavigationManager.NavigateTo($"/{nameof(ViewSubmittedData)}/{id}");
        }
    }
}
