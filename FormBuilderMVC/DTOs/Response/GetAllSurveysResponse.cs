using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.DTOs.Response
{
    public class GetAllSurveysResponse
    {
        public List<SurveysDto> Surveys { get; set; } = [];
    }
}
