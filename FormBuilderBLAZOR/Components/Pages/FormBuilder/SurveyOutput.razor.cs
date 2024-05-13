using FormBuilderBLAZOR.Components.Pages.Survey;
using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Survey;
using FormBuilderDTO.DTOs.UserSubmitDetails;
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
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private ISurveyRepository SurveyRepository { get; set; } = default!;

        [Inject]
        private IUserSubmitDetailsRepository UserSubmitDetailsRepository { get; set; } = default!;

        private string HtmlTags { get; set; } = string.Empty;

        private GetSurveyResponse _getSurveyResponse = new();

        protected async override Task OnInitializedAsync()
        {
            _getSurveyResponse = await SurveyRepository.GetSurveyById(new GetSurveyRequest { Id = Id });

            HtmlTags = HtmlHelper.MergeHtmlTags(_getSurveyResponse.Inputs);
        }

        public async Task SubmitForm()
        {
            // Used to store unique id of the user (This can be replaced when using Authentication by passing unique id of the user)
            string userId = string.Empty;

            // Excluding unwanted form values
            var excludeKeys = new HashSet<string>
                {
                    "__RequestVerificationToken", "_handler"
                };

            Dictionary<string, string> formValues = [];

            var request = httpContextAccessor?.HttpContext?.Request;

            if (request is not null)
            {
                foreach (var key in request.Form.Keys)
                {
                    if (!excludeKeys.Contains(key))
                    {
                        var value = request.Form[key].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (key == "userId")
                            {
                                userId = value;
                            }
                            else
                            {
                                formValues[key] = value;
                            }
                        }
                    }
                }

                foreach (var file in request.Form.Files)
                {
                    using var memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);

                    string base64String = Convert.ToBase64String(memoryStream.ToArray());
                    formValues[file.Name] = base64String;
                }

                var createUserSubmitDetailsRequest = new CreateUserSubmitDetailsRequest
                {
                    UserSubmitDetails = new()
                    {
                        SurveyId = _getSurveyResponse.Survey.Id,
                        DateCreatedBy = DateTime.Now,
                        UserId = userId
                    },
                    UserData = formValues.Select(data => new UserDataDtos
                    {
                        Label = data.Key,
                        Value = data.Value
                    }).ToList()
                };

                await UserSubmitDetailsRepository.CreateUserSubmitDetails(createUserSubmitDetailsRequest);

                NavigationManager.NavigateTo($"/{nameof(Surveys)}");
            }
        }
    }
}
