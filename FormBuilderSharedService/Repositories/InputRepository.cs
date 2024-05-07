using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Input;
using FormBuilderSharedService.DbContexts;
using FormBuilderSharedService.Models;
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

        // Get inputs based on survey id
        public async Task<GetInputsBasedOnSurveyIdResponse> GetInputsBySurveyId(GetInputsBasedOnSurveyIdRequest request)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);

            var inputs = await _context.TblInputs
                .Include(input => input.Survey)
                .Where(input => input.Survey.EndDate >= currentDate && input.Survey.OpenDate <= currentDate)
                .Where(input => input.SurveyId == request.SurveyId)
                .Select(input => new InputsDto
                {
                    Id = input.Id,
                    SurveyId = input.SurveyId,
                    OrderNo = input.OrderNo,
                    ControlId = input.ControlId,
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
