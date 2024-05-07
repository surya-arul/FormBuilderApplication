using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Response
{
    public class GetAllSurveysResponse
    {
        public List<SurveysDto> Surveys { get; set; } = [];
    }
}
