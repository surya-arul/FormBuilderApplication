using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Input;
using FormBuilderDTO.DTOs.Survey;
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

        // Get survey and input with control name based on id
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
                    Inputs = survey.TblInputs.Select(input => new GetInputWithControl
                    {
                        Id = input.Id,
                        SurveyId = input.SurveyId,
                        OrderNo = input.OrderNo,
                        ControlId = input.ControlId,
                        Control = new ControlsDto
                        {
                            InternalName = input.Control.InternalName,
                        }
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
                TblInputs = request.Inputs.Select(input => new TblInput
                {
                    ControlId = input.ControlId,
                    OrderNo = input.OrderNo,
                }).ToList()
            };

            _context.TblSurveys.Add(survey);
            await _context.SaveChangesAsync();

            return new CreateSurveyResponse { IsCreated = true };
        }

        // Update survey
        public async Task<UpdateSurveyResponse> UpdateSurvey(UpdateSurveyRequest request)
        {
            var existingSurvey = await _context.TblSurveys
                    .Include(input => input.TblInputs)
                    .FirstOrDefaultAsync(survey => survey.Id == request.Survey.Id);

            if (existingSurvey is null)
            {
                return new UpdateSurveyResponse
                {
                    IsUpdated = false,
                };
            }

            // Update survey details
            existingSurvey.Title = request.Survey.Title;
            existingSurvey.OpenDate = request.Survey.OpenDate;
            existingSurvey.EndDate = request.Survey.EndDate;
            existingSurvey.FormMethod = request.Survey.FormMethod;
            existingSurvey.FormAction = request.Survey.FormAction;

            // Remove inputs that are not in the updated list
            foreach (var existingInput in existingSurvey.TblInputs)
            {
                if (!request.Inputs.Any(input => input.Id == existingInput.Id))
                {
                    _context.TblInputs.Remove(existingInput);
                }
            }

            // Add new inputs or update existing ones
            foreach (var updatedInput in request.Inputs)
            {
                var existingInput = existingSurvey.TblInputs.FirstOrDefault(c => c.Id == updatedInput.Id);
                if (existingInput != null)
                {
                    // Update existing input
                    existingInput.ControlId = updatedInput.ControlId;
                    existingInput.OrderNo = updatedInput.OrderNo;
                }
                else
                {
                    // Add new input
                    existingSurvey.TblInputs.Add(new TblInput
                    {
                        ControlId = updatedInput.ControlId,
                        OrderNo = updatedInput.OrderNo,
                    });
                }
            }            

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