using FormBuilderDTO.DTOs.UserSubmitDetails;
using FormBuilderSharedService.DbContexts;
using FormBuilderSharedService.Models;

namespace FormBuilderSharedService.Repositories
{
    public interface IUserSubmitDataRepository
    {
        //Task<GetControlResponse> GetControlById(GetControlRequest request);
        //Task<GetAllControlsResponse> GetAllControls();
        //Task<List<KeyValuePair<string, string>>> GetAllControlsForDropDown();
        Task<CreateUserSubmitDetailsResponse> CreateUserSubmitDetails(CreateUserSubmitDetailsRequest request);
        //Task<UpdateControlResponse> UpdateControl(UpdateControlRequest request);
        //Task<DeleteControlResponse> DeleteControl(DeleteControlRequest request);
    }
    public class UserSubmitDataRepository : IUserSubmitDataRepository
    {
        private readonly ApplicationDbContext _context;

        public UserSubmitDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CreateUserSubmitDetailsResponse> CreateUserSubmitDetails(CreateUserSubmitDetailsRequest request)
        {
            if (request is null && request?.UserSubmitDetails is null)
            {
                return new CreateUserSubmitDetailsResponse
                {
                    IsCreated = false,
                };
            }

            var userSubmitDetails = new TblUserSubmitDetail
            {
                SurveyId = request.UserSubmitDetails.SurveyId,
                UserId = request.UserSubmitDetails.UserId,
                DateCreatedBy = request.UserSubmitDetails.DateCreatedBy,
                TblUserData = request.UserData.Select(data => new TblUserDatum
                {
                    Label = data.Label,
                    Value = data.Value,
                }).ToList()
            };

            _context.TblUserSubmitDetails.Add(userSubmitDetails);
            await _context.SaveChangesAsync();

            return new CreateUserSubmitDetailsResponse { IsCreated = true };
        }
    }
}
