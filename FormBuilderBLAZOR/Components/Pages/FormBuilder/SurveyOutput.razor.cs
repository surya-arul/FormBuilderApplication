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

        private GetSurveyResponse _getSurveyResponse = new();

        protected async override Task OnInitializedAsync()
        {
            var _getSurveyResponse = await SurveyRepository.GetSurveyById(new GetSurveyRequest { Id = Id });

            HtmlTags = HtmlHelper.MergeHtmlTags(_getSurveyResponse.Inputs);
        }

        public void SubmitForm()
        {
            Dictionary<string, string> formValues = [];

            var request = httpContextAccessor.HttpContext.Request;

            foreach (var key in request.Form.Keys)
            {
                formValues[key] = request.Form[key];
            }

            foreach (var file in request.Form.Files)
            {
                using var memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);

                string base64String = Convert.ToBase64String(memoryStream.ToArray());
                formValues[file.Name] = base64String;
            }
        }
    }
}
