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
        private readonly IUserSubmitDetailsRepository _userSubmitDetailsRepository;
        private readonly IUserDataRepository _userDataRepository;

        public FormBuilderController(ISurveyRepository surveyRepository, IUserSubmitDetailsRepository userSubmitDetailsRepository, IUserDataRepository userDataRepository)
        {
            _surveyRepository = surveyRepository;
            _userSubmitDetailsRepository = userSubmitDetailsRepository;
            _userDataRepository = userDataRepository;
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

        #endregion

        #region SubmitDynamicForm

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitDynamicForm(int surveyId)
        {
            try
            {
                // Used to store unique id of the user (This can be replaced when using Authentication by passing unique id of the user)
                string userId = string.Empty;

                // Excluding unwanted form values
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
                            if (key == "userId")
                            {
                                userId = value;
                            }
                            else
                            {
                                formValues[key] = value;
                            }
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
                        UserId = userId
                    },
                    UserData = formValues.Select(data => new UserDataDtos
                    {
                        Label = data.Key,
                        Value = data.Value
                    }).ToList()
                };

                await _userSubmitDetailsRepository.CreateUserSubmitDetails(createUserSubmitDetailsRequest);

                return RedirectToAction(nameof(SurveyController.Surveys), StringHelper.ExtractControllerName(typeof(SurveyController)));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }


        #endregion

        #region Get user submit details based on survey id

        public async Task<IActionResult> ViewUserSubmitDetails(GetUserSubmitDetailsBasedOnSurveyRequest request)
        {
            try
            {
                var response = await _userSubmitDetailsRepository.GetUserSubmitDetailsBasedOnSurvey(request);

                return View(response);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Get user data

        public async Task<IActionResult> ViewDataSubmittedByUser(GetDataSubmittedByUserRequest request)
        {
            try
            {
                var response = await _userDataRepository.GetDataSubmittedByUserBasedOnUserSubmitDetails(request);

                return View(response);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

    }
}
