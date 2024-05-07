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
        private readonly IControlRepository _controlRepository;

        public InputController(IInputRepository inputRepository, IControlRepository controlRepository)
        {
            _inputRepository = inputRepository;
            _controlRepository = controlRepository;
        }

        #region InputCRUD

        private async Task PopulateControlsListInViewData()
        {
            ViewData["ControlsList"] = await _controlRepository.GetAllControlsForDropDown();
        }

        #region Create Input

        public async Task<IActionResult> CreateInput(int surveyId)
        {
            try
            {
                await PopulateControlsListInViewData();
                return View(new CreateInputRequest() { Input = new InputsDto { SurveyId = surveyId } });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInput(CreateInputRequest createInputRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    /*// Check if OptionData has validation errors
                    if (ModelState.TryGetValue(nameof(InputsDto.OptionData), out var optionDataModelState))
                    {
                        // Clear the OptionData list if it has validation errors
                        if (optionDataModelState.Errors.Any())
                        {
                            createInputRequest.OptionData = [];
                        }
                    }*/

                    await PopulateControlsListInViewData();
                    return View(nameof(CreateInput), createInputRequest);
                }

                var response = await _inputRepository.CreateInput(createInputRequest);
                return RedirectToAction(nameof(SurveyController.SurveyDashboard), StringHelper.ExtractControllerName(typeof(SurveyController)), new { id = createInputRequest.Input.SurveyId });
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
                await PopulateControlsListInViewData();
                var existingInput = await _inputRepository.GetInputById(request);

                if (existingInput == null)
                {
                    return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = "No data to edit." });
                }

                return View(new UpdateInputRequest { Input = existingInput.Input });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInput(UpdateInputRequest updatedInputRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    /*// Check if OptionData has validation errors
                    if (ModelState.TryGetValue(nameof(InputsDto.OptionData), out var optionDataModelState))
                    {
                        // Clear the OptionData list if it has validation errors
                        if (optionDataModelState.Errors.Any())
                        {
                            updatedInputRequest.OptionData = [];
                        }
                    }*/
                    await PopulateControlsListInViewData();
                    return View(nameof(EditInput), updatedInputRequest);
                }

                var response = await _inputRepository.UpdateInput(updatedInputRequest);

                return RedirectToAction(nameof(SurveyController.SurveyDashboard), StringHelper.ExtractControllerName(typeof(SurveyController)), new { id = updatedInputRequest.Input.SurveyId });
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
