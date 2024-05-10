using FormBuilderDTO.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace FormBuilderDTO.DTOs.Survey
{
    public class UpdateSurveyRequest
    {
        [ValidateComplexType]
        public SurveysDto Survey { get; set; } = new();

        [ValidateComplexType]
        public List<InputsDto> Inputs { get; set; } = new();
    }
}
