using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Survey
{
    public class GetAllSurveysResponse
    {
        public List<SurveysDto> Surveys { get; set; } = [];
    }
}
