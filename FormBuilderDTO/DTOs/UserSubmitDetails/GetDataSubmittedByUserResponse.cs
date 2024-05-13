using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.UserSubmitDetails
{
    public class GetDataSubmittedByUserResponse
    {
        public SurveysDto Survey { get; set; } = new();
        public UserSubmitDetailsDto UserSubmitDetails { get; set; } = new();
        public List<UserDataDtos> UserDatas { get; set; } = [];
    }
}
