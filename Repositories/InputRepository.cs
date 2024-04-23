using FormBuilderMVC.DbContexts;
using FormBuilderMVC.DTOs.Base;
using FormBuilderMVC.DTOs.Request;
using FormBuilderMVC.DTOs.Response;
using FormBuilderMVC.Models;
using FormBuilderMVC.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderMVC.Repositories
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
                    InputType = input.InputType,
                    InternalName = input.InternalName,
                    DivClassName = input.DivClassName,
                    InputClassName = input.InputClassName,
                    Label = input.Label,
                    ShouldHideLabel = input.ShouldHideLabel,
                    LabelClassName = input.LabelClassName,
                    Value = input.Value,
                    IsAutofocus = input.IsAutofocus,
                    Placeholder = input.Placeholder,
                    IsRequired = input.IsRequired,
                    OptionData = !string.IsNullOrEmpty(input.OptionData) ? StringHelper.StringToList(input.OptionData, null) : new List<string>()
                }).FirstOrDefaultAsync();

            var response = new GetInputResponse
            {
                Input = input ?? new InputsDto()
            };

            return response;
        }

        // Get inputs based on survey id
        public async Task<GetInputsBasedOnSurveyIdResponse> GetInputsBySurveyId(GetInputsBasedOnSurveyIdRequest request)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);

            var inputs = await _context.TblInputs
                .Include(input => input.Survey)
                .Where(input => input.Survey.OpenDate <= currentDate && input.Survey.EndDate >= currentDate)
                .Where(input => input.SurveyId == request.SurveyId)
                .Select(input => new InputsDto
                {
                    Id = input.Id,
                    SurveyId = input.SurveyId,
                    OrderNo = input.OrderNo,
                    InputType = input.InputType,
                    InternalName = input.InternalName,
                    DivClassName = input.DivClassName,
                    InputClassName = input.InputClassName,
                    Label = input.Label,
                    ShouldHideLabel = input.ShouldHideLabel,
                    LabelClassName = input.LabelClassName,
                    Value = input.Value,
                    IsAutofocus = input.IsAutofocus,
                    Placeholder = input.Placeholder,
                    IsRequired = input.IsRequired,
                    OptionData = !string.IsNullOrEmpty(input.OptionData) ? StringHelper.StringToList(input.OptionData, null) : new List<string>()
                }).OrderBy(x => x.OrderNo).ToListAsync();

            var response = new GetInputsBasedOnSurveyIdResponse
            {
                Inputs = inputs
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
                InputType = request.Input.InputType,
                InternalName = request.Input.InternalName,
                DivClassName = request.Input.DivClassName,
                InputClassName = request.Input.InputClassName,
                Label = request.Input.Label,
                ShouldHideLabel = request.Input.ShouldHideLabel,
                LabelClassName = request.Input.LabelClassName,
                Value = request.Input.Value,
                IsAutofocus = request.Input.IsAutofocus,
                Placeholder = request.Input.Placeholder,
                IsRequired = request.Input.IsRequired,
                OptionData = request.Input.OptionData is not null ? string.Join(",", request.Input.OptionData) : string.Empty,
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
            existingInput.InputType = request.Input.InputType;
            existingInput.InternalName = request.Input.InternalName;
            existingInput.DivClassName = request.Input.DivClassName;
            existingInput.InputClassName = request.Input.InputClassName;
            existingInput.Label = request.Input.Label;
            existingInput.ShouldHideLabel = request.Input.ShouldHideLabel;
            existingInput.LabelClassName = request.Input.LabelClassName;
            existingInput.Value = request.Input.Value;
            existingInput.IsAutofocus = request.Input.IsAutofocus;
            existingInput.Placeholder = request.Input.Placeholder;
            existingInput.IsRequired = request.Input.IsRequired;
            existingInput.OptionData = request.Input.OptionData is not null ? string.Join(",", request.Input.OptionData) : string.Empty;

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
