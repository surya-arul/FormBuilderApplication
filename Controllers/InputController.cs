using FormBuilderMVC.DTOs.Base;
using FormBuilderMVC.DTOs.Request;
using FormBuilderMVC.Models;
using FormBuilderMVC.Repositories;
using FormBuilderMVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderMVC.Controllers
{
    public class InputController : Controller
    {
        private readonly IInputRepository _inputRepository;

        public InputController(IInputRepository inputRepository)
        {
            _inputRepository = inputRepository;
        }

        #region InputCRUD

        #region Create Input

        public IActionResult CreateInput(int surveyId)
        {
            try
            {
                return View(new InputsDto() { SurveyId = surveyId });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInput(InputsDto createInputRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Check if OptionData has validation errors
                    if (ModelState.TryGetValue(nameof(InputsDto.OptionData), out var optionDataModelState))
                    {
                        // Clear the OptionData list if it has validation errors
                        if (optionDataModelState.Errors.Any())
                        {
                            createInputRequest.OptionData = [];
                        }
                    }
                    return View(nameof(CreateInput), createInputRequest);
                }

                var response = await _inputRepository.CreateInput(new CreateInputRequest { Input = createInputRequest });
                return RedirectToAction(nameof(SurveyController.SurveyDashboard), StringHelper.ExtractControllerName(typeof(SurveyController)), new { id = createInputRequest.SurveyId });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Edit Input
        public async Task<IActionResult> EditInput(GetInputRequest request)
        {
            try
            {
                var existingInput = await _inputRepository.GetInputById(request);

                if (existingInput == null)
                {
                    return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = "No data to edit." });
                }

                var inputDto = existingInput.Input;

                return View(inputDto);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInput(InputsDto updatedInputRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Check if OptionData has validation errors
                    if (ModelState.TryGetValue(nameof(InputsDto.OptionData), out var optionDataModelState))
                    {
                        // Clear the OptionData list if it has validation errors
                        if (optionDataModelState.Errors.Any())
                        {
                            updatedInputRequest.OptionData = [];
                        }
                    }
                    return View(nameof(EditInput), updatedInputRequest);
                }

                var response = await _inputRepository.UpdateInput(new UpdateInputRequest() { Input = updatedInputRequest });

                return RedirectToAction(nameof(SurveyController.SurveyDashboard), StringHelper.ExtractControllerName(typeof(SurveyController)), new { id = updatedInputRequest.SurveyId });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Delete Input

        public async Task<IActionResult> DeleteInput(DeleteInputRequest deleteInputRequest, int surveyId)
        {
            try
            {
                var response = await _inputRepository.DeleteInput(deleteInputRequest);

                return RedirectToAction(nameof(SurveyController.SurveyDashboard), StringHelper.ExtractControllerName(typeof(SurveyController)), new { id = surveyId });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #endregion

    }
}
