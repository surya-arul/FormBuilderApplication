using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Input;
using FormBuilderSharedService.DbContexts;
using FormBuilderSharedService.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderSharedService.Repositories
{
    public interface IInputRepository
    {
        Task<GetInputsBasedOnSurveyIdResponse> GetInputsBySurveyId(GetInputsBasedOnSurveyIdRequest request);
    }
    public class InputRepository : IInputRepository
    {
        private readonly ApplicationDbContext _context;

        public InputRepository(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
