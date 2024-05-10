using FormBuilderDTO.DTOs.Base;
using Microsoft.AspNetCore.Components;

namespace FormBuilderBLAZOR.Components.Pages.Survey
{
    public partial class SurveyCreateAndEdit : ComponentBase
    {
        [Parameter]
        public SurveysDto Survey { get; set; } = new();
    }
}