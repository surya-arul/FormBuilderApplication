using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Survey;
using FormBuilderSharedService.Repositories;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Survey
{
    public partial class CreateSurvey : ComponentBase
    {
        private CreateSurveyRequest _createSurveyRequest = new();

        [Inject]
        private IControlRepository ControlRepository { get; set; } = default!;

        [Inject]
        private ISurveyRepository SurveyRepository { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        public List<KeyValuePair<string, string>> ControlsList { get; set; } = new List<KeyValuePair<string, string>>();

        protected override async Task OnInitializedAsync()
        {
            ControlsList = await ControlRepository.GetAllControlsForDropDown();
        }

        private void AddInput()
        {
            _createSurveyRequest.Inputs.Add(new InputsDto());
        }

        private async Task AddSurvey()
        {
            await SurveyRepository.CreateSurvey(_createSurveyRequest);
            NavigationManager.NavigateTo("/Surveys");
        }

        private async Task RemoveInput(InputsDto input)
        {
            _createSurveyRequest.Inputs.Remove(input);
            await InvokeAsync(StateHasChanged);
        }
    }
}
