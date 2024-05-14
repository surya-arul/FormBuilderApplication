using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Input;
using FormBuilderDTO.DTOs.Survey;
using FormBuilderSharedService.DbContexts;
using FormBuilderSharedService.Models;
using FormBuilderSharedService.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderSharedService.Repositories
{
    public interface ISurveyRepository
    {
        Task<GetAllSurveysResponse> GetAllSurveys();
        Task<GetPublishedSurveysResponse> GetPublishedSurveys();
        Task<GetSurveyResponse> GetSurveyById(GetSurveyRequest request);
        Task<GetPublishedSurveyResponse> GetPublishedSurveyById(GetSurveyRequest request);
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

        // Get published surveys (filter using open date & end date)
        public async Task<GetPublishedSurveysResponse> GetPublishedSurveys()
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);

            var publishedSurveys = await _context.TblSurveys
                .Where(survey => survey.OpenDate <= currentDate && survey.EndDate >= currentDate)
                .Select(survey => new SurveysDto
                {
                    Id = survey.Id,
                    Title = survey.Title,
                    OpenDate = survey.OpenDate,
                    EndDate = survey.EndDate
                }).ToListAsync();

            var response = new GetPublishedSurveysResponse
            {
                Surveys = publishedSurveys
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
                    },
                    Inputs = survey.TblInputs.Select(input => new GetInputWithControl
                    {
                        Id = input.Id,
                        SurveyId = input.SurveyId,
                        OrderNo = input.OrderNo,
                        ControlId = input.ControlId,
                        Control = new ControlsDto
                        {
                            Id = input.Control.Id,
                            InternalName = input.Control.InternalName,
                            InputType = input.Control.InputType,
                            DivClassName = input.Control.DivClassName,
                            InputClassName = input.Control.InputClassName,
                            Label = input.Control.Label,
                            ShouldHideLabel = input.Control.ShouldHideLabel,
                            LabelClassName = input.Control.LabelClassName,
                            Value = input.Control.Value,
                            IsAutofocus = input.Control.IsAutofocus,
                            Placeholder = input.Control.Placeholder,
                            IsRequired = input.Control.IsRequired,
                            OptionData = !string.IsNullOrEmpty(input.Control.OptionData) ? StringHelper.StringToList(input.Control.OptionData, null) : new List<string>()
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

        // Get published survey, input with control name based on id (filter using open date & end date)
        public async Task<GetPublishedSurveyResponse> GetPublishedSurveyById(GetSurveyRequest request)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);

            var publishedSurveyData = await _context.TblSurveys
                .Where(survey => survey.OpenDate <= currentDate && survey.EndDate >= currentDate)
                .Where(survey => survey.Id == request.Id)
                .Select(survey => new
                {
                    Survey = new SurveysDto
                    {
                        Id = survey.Id,
                        Title = survey.Title,
                        OpenDate = survey.OpenDate,
                        EndDate = survey.EndDate,
                    },
                    Inputs = survey.TblInputs.Select(input => new GetInputWithControl
                    {
                        Id = input.Id,
                        SurveyId = input.SurveyId,
                        OrderNo = input.OrderNo,
                        ControlId = input.ControlId,
                        Control = new ControlsDto
                        {
                            Id = input.Control.Id,
                            InternalName = input.Control.InternalName,
                            InputType = input.Control.InputType,
                            DivClassName = input.Control.DivClassName,
                            InputClassName = input.Control.InputClassName,
                            Label = input.Control.Label,
                            ShouldHideLabel = input.Control.ShouldHideLabel,
                            LabelClassName = input.Control.LabelClassName,
                            Value = input.Control.Value,
                            IsAutofocus = input.Control.IsAutofocus,
                            Placeholder = input.Control.Placeholder,
                            IsRequired = input.Control.IsRequired,
                            OptionData = !string.IsNullOrEmpty(input.Control.OptionData) ? StringHelper.StringToList(input.Control.OptionData, null) : new List<string>()
                        }
                    }).OrderBy(x => x.OrderNo).ToList()
                }).FirstOrDefaultAsync();

            var response = new GetPublishedSurveyResponse
            {
                Survey = publishedSurveyData?.Survey ?? new(),
                Inputs = publishedSurveyData?.Inputs ?? []
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
                    await _context.SaveChangesAsync();
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