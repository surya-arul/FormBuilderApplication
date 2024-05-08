using FormBuilderDTO.DTOs.Survey;
using FormBuilderSharedService.Models;
using FormBuilderSharedService.Repositories;
using FormBuilderSharedService.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderMVC.Controllers
{
    public class FormBuilderController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;

        public FormBuilderController(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        #region FormDesign
        public async Task<IActionResult> PreviewSurvey(GetSurveyRequest request)
        {
            try
            {
                var response = await _surveyRepository.GetSurveyById(request);

                if (response is null || response.Inputs.Count is 0)
                {
                    return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = "No data to generate survey or current date is not lies between open/end date." });
                }

                string allHtml = HtmlHelper.GenerateForm(response.Inputs, response.Survey);

                ViewBag.InputTag = allHtml;

                return View(nameof(SurveyOutput));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        public IActionResult SurveyOutput()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        } 

        #endregion
    }
}
