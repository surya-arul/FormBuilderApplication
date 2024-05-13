using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.UserSubmitDetails
{
    public class CreateUserSubmitDetailsRequest
    {
        public UserSubmitDetailsDto UserSubmitDetails { get; set; } = new();

        public List<UserDataDtos> UserData { get; set; } = [];
    }
}
