using FormBuilderMVC.DbContexts;
using FormBuilderMVC.DTOs.Base;
using FormBuilderMVC.DTOs.Request;
using FormBuilderMVC.DTOs.Response;
using FormBuilderMVC.Models;
using FormBuilderMVC.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderMVC.Repositories
{
    public interface IControlRepository
    {
        Task<GetControlResponse> GetControlById(GetControlRequest request);
        Task<GetAllControlsResponse> GetAllControls();
        Task<List<KeyValuePair<string, string>>> GetAllControlsForDropDown();
        Task<CreateControlResponse> CreateControl(CreateControlRequest request);
        Task<UpdateControlResponse> UpdateControl(UpdateControlRequest request);
        Task<DeleteControlResponse> DeleteControl(DeleteControlRequest request);
    }
    public class ControlRepository : IControlRepository
    {
        private readonly ApplicationDbContext _context;

        public ControlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get control based on id
        public async Task<GetControlResponse> GetControlById(GetControlRequest request)
        {
            var control = await _context.TblControls
                .Where(control => control.Id == request.Id)
                .Select(control => new ControlsDto
                {
                    Id = control.Id,
                    InternalName = control.InternalName,
                    InputType = control.InputType,
                    DivClassName = control.DivClassName,
                    InputClassName = control.InputClassName,
                    Label = control.Label,
                    ShouldHideLabel = control.ShouldHideLabel,
                    LabelClassName = control.LabelClassName,
                    Value = control.Value,
                    IsAutofocus = control.IsAutofocus,
                    Placeholder = control.Placeholder,
                    IsRequired = control.IsRequired,
                    OptionData = !string.IsNullOrEmpty(control.OptionData) ? StringHelper.StringToList(control.OptionData, null) : new List<string>()
                }).FirstOrDefaultAsync();

            var response = new GetControlResponse
            {
                Control = control ?? new ControlsDto()
            };

            return response;
        }

        // Get all controls
        public async Task<GetAllControlsResponse> GetAllControls()
        {
            var controls = await _context.TblControls
                .Select(control => new ControlsDto
                {
                    Id = control.Id,
                    InternalName = control.InternalName,
                    InputType = control.InputType,
                    DivClassName = control.DivClassName,
                    InputClassName = control.InputClassName,
                    Label = control.Label,
                    ShouldHideLabel = control.ShouldHideLabel,
                    LabelClassName = control.LabelClassName,
                    Value = control.Value,
                    IsAutofocus = control.IsAutofocus,
                    Placeholder = control.Placeholder,
                    IsRequired = control.IsRequired,
                    OptionData = !string.IsNullOrEmpty(control.OptionData) ? StringHelper.StringToList(control.OptionData, null) : new List<string>()
                }).ToListAsync();

            var response = new GetAllControlsResponse
            {
                Controls = controls
            };

            return response;
        }

        // Get all controls for dropdown
        public async Task<List<KeyValuePair<string, string>>> GetAllControlsForDropDown()
        {
            var controls = await _context.TblControls
                .Select(control => new ControlsDto
                {
                    Id = control.Id,
                    InternalName = control.InternalName,
                }).ToListAsync();

            var response = controls
                .Select(control => new KeyValuePair<string, string>(control.Id.ToString(), control.InternalName))
                .ToList();

            return response;
        }

        // Create input
        public async Task<CreateControlResponse> CreateControl(CreateControlRequest request)
        {
            if (request is null)
            {
                return new CreateControlResponse
                {
                    IsCreated = false,
                };
            }

            var control = new TblControl
            {
                InternalName = request.Control.InternalName,
                InputType = request.Control.InputType,
                DivClassName = request.Control.DivClassName,
                InputClassName = request.Control.InputClassName,
                Label = request.Control.Label,
                ShouldHideLabel = request.Control.ShouldHideLabel,
                LabelClassName = request.Control.LabelClassName,
                Value = request.Control.Value,
                IsAutofocus = request.Control.IsAutofocus,
                Placeholder = request.Control.Placeholder,
                IsRequired = request.Control.IsRequired,
                OptionData = request.Control.OptionData is not null ? string.Join(",", request.Control.OptionData) : string.Empty,
            };

            _context.TblControls.Add(control);
            await _context.SaveChangesAsync();

            return new CreateControlResponse { IsCreated = true };
        }

        // Update input
        public async Task<UpdateControlResponse> UpdateControl(UpdateControlRequest request)
        {
            var existingControl = await _context.TblControls
                    .Where(control => control.Id == request.Control.Id)
                    .FirstOrDefaultAsync();

            if (existingControl is null)
            {
                return new UpdateControlResponse
                {
                    IsUpdated = false,
                };
            }

            existingControl.InternalName = request.Control.InternalName;
            existingControl.InputType = request.Control.InputType;
            existingControl.DivClassName = request.Control.DivClassName;
            existingControl.InputClassName = request.Control.InputClassName;
            existingControl.Label = request.Control.Label;
            existingControl.ShouldHideLabel = request.Control.ShouldHideLabel;
            existingControl.LabelClassName = request.Control.LabelClassName;
            existingControl.Value = request.Control.Value;
            existingControl.IsAutofocus = request.Control.IsAutofocus;
            existingControl.Placeholder = request.Control.Placeholder;
            existingControl.IsRequired = request.Control.IsRequired;
            existingControl.OptionData = request.Control.OptionData is not null ? string.Join(",", request.Control.OptionData) : string.Empty;

            await _context.SaveChangesAsync();

            return new UpdateControlResponse { IsUpdated = true };
        }

        // Delete input
        public async Task<DeleteControlResponse> DeleteControl(DeleteControlRequest request)
        {
            var existingControl = await _context.TblControls.FirstOrDefaultAsync(control => control.Id == request.Id);

            if (existingControl is null)
            {
                return new DeleteControlResponse
                {
                    IsDeleted = false,
                };
            }

            _context.TblControls.Remove(existingControl);
            await _context.SaveChangesAsync();

            return new DeleteControlResponse { IsDeleted = true };
        }
    }
}