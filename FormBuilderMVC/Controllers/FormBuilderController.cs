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

                string allHtml = HtmlHelper.MergeHtmlTags(response.Inputs);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit()
        {
            try
            {
                Dictionary<string, string> formValues = [];

                foreach (var key in Request.Form.Keys)
                {
                    formValues[key] = Request.Form[key];
                }

                foreach (var file in Request.Form.Files)
                {
                    using var memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);

                    string base64String = Convert.ToBase64String(memoryStream.ToArray());
                    formValues[file.Name] = base64String;
                }

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
