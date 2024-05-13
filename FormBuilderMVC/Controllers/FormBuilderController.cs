using FormBuilderDTO.DTOs.Base;
using FormBuilderDTO.DTOs.Survey;
using FormBuilderDTO.DTOs.UserSubmitDetails;
using FormBuilderSharedService.Models;
using FormBuilderSharedService.Repositories;
using FormBuilderSharedService.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderMVC.Controllers
{
    public class FormBuilderController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IUserSubmitDataRepository _userSubmitDataRepository;

        public FormBuilderController(ISurveyRepository surveyRepository, IUserSubmitDataRepository userSubmitDataRepository)
        {
            _surveyRepository = surveyRepository;
            _userSubmitDataRepository = userSubmitDataRepository;
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

                return View(nameof(SurveyOutput), new SurveysDto { Id = response.Survey.Id });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        public IActionResult SurveyOutput(SurveysDto survey)
        {
            try
            {
                return View(survey);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(int surveyId)
        {
            try
            {
                var excludeKeys = new HashSet<string>
                {
                    "__RequestVerificationToken"
                };

                Dictionary<string, string> formValues = [];

                foreach (var key in Request.Form.Keys)
                {
                    if (!excludeKeys.Contains(key))
                    {
                        var value = Request.Form[key].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            formValues[key] = value;
                        }
                    }
                }

                foreach (var file in Request.Form.Files)
                {
                    using var memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);

                    string base64String = Convert.ToBase64String(memoryStream.ToArray());
                    formValues[file.Name] = base64String;
                }

                var createUserSubmitDetailsRequest = new CreateUserSubmitDetailsRequest
                {
                    UserSubmitDetails = new()
                    {
                        SurveyId = surveyId,
                        DateCreatedBy = DateTime.Now,
                        UserId = "hello"
                    },
                    UserData = formValues.Select(data => new UserDataDtos
                    {
                        Label = data.Key,
                        Value = data.Value
                    }).ToList()
                };

                await _userSubmitDataRepository.CreateUserSubmitDetails(createUserSubmitDetailsRequest);

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
