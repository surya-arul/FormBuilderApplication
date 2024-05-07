using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Input;
using FormBuilderSharedService.DbContexts;
using FormBuilderSharedService.Models;
using FormBuilderSharedService.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderSharedService.Repositories
{
    public interface IInputRepository
    {
        Task<GetInputResponse> GetInputById(GetInputRequest request);
        Task<GetInputsBasedOnSurveyIdResponse> GetInputsBySurveyId(GetInputsBasedOnSurveyIdRequest request);
        Task<CreateInputResponse> CreateInput(CreateInputRequest request);
        Task<UpdateInputResponse> UpdateInput(UpdateInputRequest request);
        Task<DeleteInputResponse> DeleteInput(DeleteInputRequest request);
    }
    public class InputRepository : IInputRepository
    {
        private readonly ApplicationDbContext _context;

        public InputRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get input based on id
        public async Task<GetInputResponse> GetInputById(GetInputRequest request)
        {
            var input = await _context.TblInputs
                .Where(input => input.Id == request.Id)
                .Select(input => new InputsDto
                {
                    Id = input.Id,
                    SurveyId = input.SurveyId,
                    OrderNo = input.OrderNo,
                    ControlId = input.ControlId,
                }).FirstOrDefaultAsync();

            var response = new GetInputResponse
            {
                Input = input ?? new InputsDto()
            };

            return response;
        }

        // Get inputs and survey based on survey id
        public async Task<GetInputsBasedOnSurveyIdResponse> GetInputsBySurveyId(GetInputsBasedOnSurveyIdRequest request)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);

            var inputsAndSurvey = await _context.TblInputs
                .Include(input => input.Survey)
                .Where(input => input.Survey.EndDate >= currentDate && input.Survey.OpenDate <= currentDate)
                .Where(input => input.SurveyId == request.SurveyId)
                .Select(survey => new
                {
                    Survey = new SurveysDto
                    {
                        Id = survey.Survey.Id,
                        Title = survey.Survey.Title,
                        OpenDate = survey.Survey.OpenDate,
                        EndDate = survey.Survey.EndDate,
                        FormMethod = survey.Survey.FormMethod,
                        FormAction = survey.Survey.FormAction,
                    },
                    Inputs = survey.Survey.TblInputs.Select(input => new GetInputWithControl
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

            var response = new GetInputsBasedOnSurveyIdResponse
            {
                Survey = inputsAndSurvey?.Survey ?? new(),
                Inputs = inputsAndSurvey?.Inputs ?? []
            };

            return response;
        }

        // Create input
        public async Task<CreateInputResponse> CreateInput(CreateInputRequest request)
        {
            if (request is null)
            {
                return new CreateInputResponse
                {
                    IsCreated = false,
                };
            }

            var input = new TblInput
            {
                SurveyId = request.Input.SurveyId,
                OrderNo = request.Input.OrderNo,
                ControlId = request.Input.ControlId,
            };

            _context.TblInputs.Add(input);
            await _context.SaveChangesAsync();

            return new CreateInputResponse { IsCreated = true };
        }

        // Update input
        public async Task<UpdateInputResponse> UpdateInput(UpdateInputRequest request)
        {
            var existingInput = await _context.TblInputs
                    .Where(input => input.Id == request.Input.Id)
                    .FirstOrDefaultAsync();

            if (existingInput is null)
            {
                return new UpdateInputResponse
                {
                    IsUpdated = false,
                };
            }

            existingInput.SurveyId = request.Input.SurveyId;
            existingInput.OrderNo = request.Input.OrderNo;
            existingInput.ControlId = request.Input.ControlId;

            await _context.SaveChangesAsync();

            return new UpdateInputResponse { IsUpdated = true };
        }

        // Delete input
        public async Task<DeleteInputResponse> DeleteInput(DeleteInputRequest request)
        {
            var existingInput = await _context.TblInputs.FirstOrDefaultAsync(input => input.Id == request.Id);

            if (existingInput is null)
            {
                return new DeleteInputResponse
                {
                    IsDeleted = false,
                };
            }

            _context.TblInputs.Remove(existingInput);
            await _context.SaveChangesAsync();

            return new DeleteInputResponse { IsDeleted = true };
        }
    }
}
