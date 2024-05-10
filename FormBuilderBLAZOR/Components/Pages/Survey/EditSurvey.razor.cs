using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Survey;
using FormBuilderSharedService.Repositories;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Survey
{
    public partial class EditSurvey
    {
        [Parameter]
        public int Id { get; set; }

        private UpdateSurveyRequest _updateSurveyRequest = new();

        [Inject]
        private IControlRepository ControlRepository { get; set; } = default!;

        [Inject]
        private ISurveyRepository SurveyRepository { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        public List<KeyValuePair<string, string>> ControlsList { get; set; } = [];

        protected override async Task OnInitializedAsync()
        {
            ControlsList = await ControlRepository.GetAllControlsForDropDown();

            var survey = await SurveyRepository.GetSurveyById(new GetSurveyRequest { Id = Id });

            _updateSurveyRequest = new UpdateSurveyRequest
            {
                Survey = survey.Survey,
                Inputs = survey.Inputs.Select(input => new InputsDto
                {
                    Id = input.Id,
                    SurveyId = input.SurveyId,
                    OrderNo = input.OrderNo,
                    ControlId = input.ControlId,
                }).ToList()
            };

        }

        private void AddInput()
        {
            _updateSurveyRequest.Inputs.Add(new InputsDto());
        }

        private async Task UpdateSurvey()
        {
            await SurveyRepository.UpdateSurvey(_updateSurveyRequest);
            NavigationManager.NavigateTo("/Surveys");
        }

        private async Task RemoveInput(InputsDto input)
        {
            _updateSurveyRequest.Inputs.Remove(input);
            await InvokeAsync(StateHasChanged);
        }
    }
}
