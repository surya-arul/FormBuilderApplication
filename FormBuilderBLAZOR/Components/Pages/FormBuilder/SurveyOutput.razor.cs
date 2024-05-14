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

        private GetPublishedSurveyResponse _getPublishedSurveyResponse = new();

        protected async override Task OnInitializedAsync()
        {
            _getPublishedSurveyResponse = await SurveyRepository.GetPublishedSurveyById(new GetSurveyRequest { Id = Id });

            HtmlTags = HtmlHelper.MergeHtmlTags(_getPublishedSurveyResponse.Inputs);
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

            List<(string label, string value, byte[]? byteValue)> formValues = [];

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
                                formValues.Add((key, value, null));
                            }
                        }
                    }
                }

                foreach (var file in request.Form.Files)
                {
                    using var memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);

                    formValues.Add((file.Name, file.FileName, memoryStream.ToArray()));
                }

                var createUserSubmitDetailsRequest = new CreateUserSubmitDetailsRequest
                {
                    UserSubmitDetails = new()
                    {
                        SurveyId = _getPublishedSurveyResponse.Survey.Id,
                        DateCreatedBy = DateTime.Now,
                        UserId = userId
                    },
                    UserData = formValues.Select(data => new UserDataDtos
                    {
                        Label = data.label,
                        Value = data.value,
                        ByteValue = data.byteValue is not null ? data.byteValue : null
                    }).ToList()
                };

                await UserSubmitDetailsRepository.CreateUserSubmitDetails(createUserSubmitDetailsRequest);

                NavigationManager.NavigateTo($"/{nameof(Surveys)}");
            }
        }
    }
}
