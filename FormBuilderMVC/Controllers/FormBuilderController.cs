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
                var response = await _surveyRepository.GetPublishedSurveyById(request);

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

                List<(string label, string value, byte[]? byteValue)> formValues = [];

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
                                formValues.Add((key, value, null));
                            }
                        }
                    }
                }

                foreach (var file in Request.Form.Files)
                {
                    using var memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);

                    formValues.Add((file.Name, file.FileName, memoryStream.ToArray()));
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
                        Label = data.label,
                        Value = data.value,
                        ByteValue = data.byteValue is not null ? data.byteValue : null
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
