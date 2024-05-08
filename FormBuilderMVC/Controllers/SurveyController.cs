using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Input;
using FormBuilderDTO.DTOs.Survey;
using FormBuilderSharedService.Models;
using FormBuilderSharedService.Repositories;
using FormBuilderSharedService.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderMVC.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IControlRepository _controlRepository;
        private CreateSurveyRequest _createSurveyRequest;

        public SurveyController(ISurveyRepository surveyRepository, IControlRepository controlRepository)
        {
            _surveyRepository = surveyRepository;
            _controlRepository = controlRepository;
            _createSurveyRequest = new CreateSurveyRequest();
        }

        private async Task PopulateControlsListInViewData()
        {
            ViewData["ControlsList"] = await _controlRepository.GetAllControlsForDropDown();
        }

        #region SurveyCRUD

        #region Get Survey

        public async Task<IActionResult> Surveys()
        {
            try
            {
                var response = await _surveyRepository.GetAllSurveys();
                return View(response);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Create Survey

        public async Task<IActionResult> CreateSurvey()
        {
            try
            {
                await PopulateControlsListInViewData();
                _createSurveyRequest = new CreateSurveyRequest();
                return View(_createSurveyRequest);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSurvey(CreateSurveyRequest createSurveyRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await PopulateControlsListInViewData();
                    return View(nameof(CreateSurvey), createSurveyRequest);
                }

                var response = await _surveyRepository.CreateSurvey(createSurveyRequest);
                return RedirectToAction(nameof(Surveys));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Edit Survey

        public async Task<IActionResult> EditSurvey(GetSurveyRequest request)
        {
            try
            {
                await PopulateControlsListInViewData();
                var survey = await _surveyRepository.GetSurveyById(request);

                if (survey is null)
                {
                    return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = "No data to edit." });
                }

                return View(new UpdateSurveyRequest 
                { 
                    Survey = survey.Survey, 
                    Inputs = survey.Inputs.Select(input => new InputsDto
                    {
                        Id = input.Id,
                        SurveyId = input.SurveyId,
                        OrderNo = input.OrderNo,
                        ControlId = input.ControlId,
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSurvey(UpdateSurveyRequest updatedSurveyRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await PopulateControlsListInViewData();
                    return View(nameof(EditSurvey), updatedSurveyRequest);
                }

                var response = await _surveyRepository.UpdateSurvey(updatedSurveyRequest);

                return RedirectToAction(nameof(Surveys));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Delete Survey

        public async Task<IActionResult> DeleteSurvey(DeleteSurveyRequest deleteSurveyRequest)
        {
            try
            {
                var response = await _surveyRepository.DeleteSurvey(deleteSurveyRequest);

                return RedirectToAction(nameof(Surveys));
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
