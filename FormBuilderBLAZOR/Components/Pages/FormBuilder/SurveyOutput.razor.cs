using FormBuilderDTO.DTOs.Survey;
using FormBuilderSharedService.Repositories;
using FormBuilderSharedService.Utilities;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.FormBuilder
{
    public partial class SurveyOutput : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private ISurveyRepository SurveyRepository { get; set; } = default!;

        private string HtmlTags { get; set; } = string.Empty;

        private GetAllSurveysResponse _getAllSurveysResponse = new();

        protected async override Task OnInitializedAsync()
        {
            var response = await SurveyRepository.GetSurveyById(new GetSurveyRequest { Id = Id });

            HtmlTags = HtmlHelper.GenerateForm(response.Inputs, response.Survey);
        }
    }
}
