using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.UserSubmitDetails;
using FormBuilderSharedService.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderSharedService.Repositories
{
    public interface IUserDataRepository
    {
        Task<GetDataSubmittedByUserResponse> GetDataSubmittedByUserBasedOnUserSubmitDetails(GetDataSubmittedByUserRequest request);
    }
    public class UserDataRepository : IUserDataRepository
    {
        private readonly ApplicationDbContext _context;

        public UserDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetDataSubmittedByUserResponse> GetDataSubmittedByUserBasedOnUserSubmitDetails(GetDataSubmittedByUserRequest request)
        {
            var userSubmittedDetails = await _context.TblUserData
                .Where(userData => userData.UserSubmitDetailsId == request.UserSubmitDetailsId)
                .Include(userSubmittedDetails => userSubmittedDetails.UserSubmitDetails)
                .Include(userSubmittedDetails => userSubmittedDetails.UserSubmitDetails.Survey)
                .Select(userDetails => new GetDataSubmittedByUserResponse
                {
                    Survey = new()
                    {
                        Title = userDetails.UserSubmitDetails.Survey.Title,
                    },
                    UserSubmitDetails = new()
                    {
                        UserId = userDetails.UserSubmitDetails.UserId,
                        DateCreatedBy = userDetails.UserSubmitDetails.DateCreatedBy,
                    },
                    UserDatas = userDetails.UserSubmitDetails.TblUserData
                    .Select(userData => new UserDataDtos
                    {
                        Label = userData.Label,
                        Value = userData.Value,
                        ByteValue = userData.ByteValue,
                    }).ToList()
                }).FirstOrDefaultAsync();

            return userSubmittedDetails ?? new();
        }
    }
}
