using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Request;
using FormBuilderDTO.DTOs.Response;
using FormBuilderSharedService.DbContexts;
using FormBuilderSharedService.Models;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderSharedService.Repositories
{
    public interface ISurveyRepository
    {
        Task<GetAllSurveysResponse> GetAllSurveys();
        Task<GetSurveyResponse> GetSurveyById(GetSurveyRequest request);
        Task<CreateSurveyResponse> CreateSurvey(CreateSurveyRequest request);
        Task<UpdateSurveyResponse> UpdateSurvey(UpdateSurveyRequest request);
        Task<DeleteSurveyResponse> DeleteSurvey(DeleteSurveyRequest request);
    }
    public class SurveyRepository : ISurveyRepository
    {
        private readonly ApplicationDbContext _context;

        public SurveyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all surveys
        public async Task<GetAllSurveysResponse> GetAllSurveys()
        {

            var surveys = await _context.TblSurveys
                .Select(survey => new SurveysDto
                {
                    Id = survey.Id,
                    Title = survey.Title,
                    OpenDate = survey.OpenDate,
                    EndDate = survey.EndDate
                }).ToListAsync();

            var response = new GetAllSurveysResponse
            {
                Surveys = surveys
            };

            return response;
        }

        // Get survey based on id
        public async Task<GetSurveyResponse> GetSurveyById(GetSurveyRequest request)
        {
            var surveyData = await _context.TblSurveys
                .Where(survey => survey.Id == request.Id)
                .Select(survey => new
                {
                    Survey = new SurveysDto
                    {
                        Id = survey.Id,
                        Title = survey.Title,
                        OpenDate = survey.OpenDate,
                        EndDate = survey.EndDate,
                        FormMethod = survey.FormMethod,
                        FormAction = survey.FormAction,
                    },
                    Inputs = survey.TblInputs.Select(input => new InputsDto
                    {
                        Id = input.Id,
                        SurveyId = input.SurveyId,
                        OrderNo = input.OrderNo,
                        ControlId = input.ControlId,
                    }).OrderBy(x => x.OrderNo).ToList()
                }).FirstOrDefaultAsync();

            var response = new GetSurveyResponse
            {
                Survey = surveyData?.Survey ?? new(),
                Inputs = surveyData?.Inputs ?? []
            };


            return response;
        }

        // Create survey
        public async Task<CreateSurveyResponse> CreateSurvey(CreateSurveyRequest request)
        {
            if (request is null && request?.Survey is null)
            {
                return new CreateSurveyResponse
                {
                    IsCreated = false,
                };
            }

            var survey = new TblSurvey
            {
                Title = request.Survey.Title,
                OpenDate = request.Survey.OpenDate,
                EndDate = request.Survey.EndDate,
                FormMethod = request.Survey.FormMethod,
                FormAction = request.Survey.FormAction,
            };

            _context.TblSurveys.Add(survey);
            await _context.SaveChangesAsync();

            return new CreateSurveyResponse { IsCreated = true };
        }

        // Update survey
        public async Task<UpdateSurveyResponse> UpdateSurvey(UpdateSurveyRequest request)
        {
            var existingSurvey = await _context.TblSurveys
                    .Where(survey => survey.Id == request.Survey.Id)
                    .Include(input => input.TblInputs)
                    .FirstOrDefaultAsync();

            if (existingSurvey is null)
            {
                return new UpdateSurveyResponse
                {
                    IsUpdated = false,
                };
            }

            existingSurvey.Title = request.Survey.Title;
            existingSurvey.OpenDate = request.Survey.OpenDate;
            existingSurvey.EndDate = request.Survey.EndDate;
            existingSurvey.FormMethod = request.Survey.FormMethod;
            existingSurvey.FormAction = request.Survey.FormAction;

            await _context.SaveChangesAsync();

            return new UpdateSurveyResponse { IsUpdated = true };
        }

        // Delete survey
        public async Task<DeleteSurveyResponse> DeleteSurvey(DeleteSurveyRequest request)
        {
            var existingSurvey = await _context.TblSurveys.FirstOrDefaultAsync(survey => survey.Id == request.Id);

            if (existingSurvey is null)
            {
                return new DeleteSurveyResponse
                {
                    IsDeleted = false,
                };
            }

            _context.TblSurveys.Remove(existingSurvey);
            await _context.SaveChangesAsync();

            return new DeleteSurveyResponse { IsDeleted = true };
        }
    }
}