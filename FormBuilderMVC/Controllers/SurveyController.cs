using FormBuilderDTO.DTOs.Request;
using FormBuilderSharedService.Models;
using FormBuilderSharedService.Repositories;
using FormBuilderSharedService.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderMVC.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;

        public SurveyController(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
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

        public async Task<IActionResult> SurveyDashboard(GetSurveyRequest request)
        {
            try
            {
                var response = await _surveyRepository.GetSurveyById(request);
                return View(response);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Create Survey

        public IActionResult CreateSurvey()
        {
            try
            {
                return View(new CreateSurveyRequest());
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
                var survey = await _surveyRepository.GetSurveyById(request);

                if (survey is null)
                {
                    return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = "No data to edit." });
                }

                return View(new UpdateSurveyRequest { Survey = survey.Survey });
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
