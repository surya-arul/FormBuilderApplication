using FormBuilderDTO.DTOs.Survey;
using FormBuilderSharedService.Repositories;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Survey
{
    public partial class Surveys : ComponentBase
    {
        private GetAllSurveysResponse _getAllSurveysResponse = new();

        [Inject]
        private ISurveyRepository SurveyRepository { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            _getAllSurveysResponse = await GetAllSurveys();
        }

        private async Task<GetAllSurveysResponse> GetAllSurveys()
        {
            return await SurveyRepository.GetAllSurveys();
        }

        private void NavigateToCreateSurvey()
        {
            NavigationManager.NavigateTo("/CreateSurvey");
        }

        private void EditSurvey(int id)
        {
            NavigationManager.NavigateTo($"/EditSurvey/{id}");
        }

        private async Task DeleteSurvey(int id)
        {
            var response = await SurveyRepository.DeleteSurvey(new DeleteSurveyRequest { Id = id });

            // Refresh list
            _getAllSurveysResponse = await GetAllSurveys();
        }

        private void PreviewSurvey(int id)
        {
            NavigationManager.NavigateTo($"/SurveyOutput/{id}");
        }
    }
}
