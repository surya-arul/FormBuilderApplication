using FormBuilderBLAZOR.Components.Pages.FormBuilder;
using FormBuilderDTO.DTOs.Survey;
using FormBuilderSharedService.Repositories;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Survey
{
    public partial class PublishedSurveys : ComponentBase
    {
        private GetPublishedSurveysResponse _getPublishedSurveysResponse = new();

        [Inject]
        private ISurveyRepository SurveyRepository { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            _getPublishedSurveysResponse = await GetPublishedSurveys();
        }

        private async Task<GetPublishedSurveysResponse> GetPublishedSurveys()
        {
            return await SurveyRepository.GetPublishedSurveys();
        }

        private void PreviewSurvey(int id)
        {
            NavigationManager.NavigateTo($"/{nameof(SurveyOutput)}/{id}");
        }
    }
}
