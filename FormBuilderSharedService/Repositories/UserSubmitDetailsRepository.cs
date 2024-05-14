using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.UserSubmitDetails;
using FormBuilderSharedService.DbContexts;
using FormBuilderSharedService.Models;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderSharedService.Repositories
{
    public interface IUserSubmitDetailsRepository
    {
        Task<GetUserSubmitDetailsBasedOnSurveyResponse> GetUserSubmitDetailsBasedOnSurvey(GetUserSubmitDetailsBasedOnSurveyRequest request);
        Task<CreateUserSubmitDetailsResponse> CreateUserSubmitDetails(CreateUserSubmitDetailsRequest request);
    }
    public class UserSubmitDetailsRepository : IUserSubmitDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public UserSubmitDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserSubmitDetailsBasedOnSurveyResponse> GetUserSubmitDetailsBasedOnSurvey(GetUserSubmitDetailsBasedOnSurveyRequest request)
        {
            var userSubmitDetails = await _context.TblUserSubmitDetails
                .Where(userDetails => userDetails.SurveyId == request.SurveyId)
                .Include(survey => survey.Survey)
                .Select(userDetails => new GetUserSubmitDetailsBasedOnSurveyResponse
                {
                    Survey = new()
                    {
                        Id = userDetails.Survey.Id,
                        Title = userDetails.Survey.Title,
                    },
                    UserSubmitDetails = userDetails.Survey.TblUserSubmitDetails
                    .Select(userDetails => new UserSubmitDetailsDto
                    {
                        Id = userDetails.Id,
                        UserId = userDetails.UserId,
                        DateCreatedBy = userDetails.DateCreatedBy,
                    }).ToList()
                }).FirstOrDefaultAsync();

            return userSubmitDetails ?? new();
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
                    ByteValue = data.ByteValue,
                }).ToList()
            };

            _context.TblUserSubmitDetails.Add(userSubmitDetails);
            await _context.SaveChangesAsync();

            return new CreateUserSubmitDetailsResponse { IsCreated = true };
        }
    }
}
