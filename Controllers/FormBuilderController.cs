using FormBuilderMVC.DTOs.Request;
using FormBuilderMVC.Models;
using FormBuilderMVC.Repositories;
using FormBuilderMVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderMVC.Controllers
{
    public class FormBuilderController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IInputRepository _inputRepository;

        public FormBuilderController(ISurveyRepository surveyRepository, IInputRepository inputRepository)
        {
            _surveyRepository = surveyRepository;
            _inputRepository = inputRepository;
        }

        #region FormDesign
        public async Task<IActionResult> PreviewSurvey(GetInputsBasedOnSurveyIdRequest request)
        {
            try
            {
                var response = await _inputRepository.GetInputsBySurveyId(request);

                if (response is null || response.Inputs.Count is 0)
                {
                    return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = "No data to generate survey." });
                }

                var surveyResponse = await _surveyRepository.GetSurveyById(new GetSurveyRequest { Id = request.SurveyId });

                string allHtml = HtmlHelper.GenerateForm(response.Inputs, surveyResponse.Survey);

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
