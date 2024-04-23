using FormBuilderMVC.DTOs.Base;
using FormBuilderMVC.DTOs.Request;
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
                return RedirectToAction("Error", ex);
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
                return RedirectToAction("Error", ex);
            }
        }

        #endregion

        #region Create Survey

        public IActionResult CreateSurvey()
        {
            try
            {
                return View(new SurveysDto());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSurvey(SurveysDto createSurveyRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(nameof(CreateSurvey), createSurveyRequest);
                }

                var response = await _surveyRepository.CreateSurvey(new CreateSurveyRequest { Survey = createSurveyRequest });
                return RedirectToAction(nameof(Surveys));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        #endregion

        #region Edit Survey

        public async Task<IActionResult> EditSurvey(GetSurveyRequest request)
        {
            try
            {
                var survey = await _surveyRepository.GetSurveyById(request);

                if (survey == null)
                {
                    return NotFound();
                }

                var surveyDto = survey.Survey;

                return View(surveyDto);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSurvey(SurveysDto updatedSurveyRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(nameof(EditSurvey), updatedSurveyRequest);
                }

                var response = await _surveyRepository.UpdateSurvey(new UpdateSurveyRequest() { Survey = updatedSurveyRequest });

                return RedirectToAction(nameof(Surveys));
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
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
                return RedirectToAction("Error", ex);
            }
        }

        #endregion

        #endregion

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
                return RedirectToAction("Error", ex);
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
                    return View(nameof(CreateInput), createInputRequest);
                }

                /*if (!Enum.TryParse(model.InputType, true, out HtmlInputType inputType))
                {
                    return RedirectToAction("Error");
                }*/

                var response = await _inputRepository.CreateInput(new CreateInputRequest { Input = createInputRequest });
                return RedirectToAction(nameof(SurveyDashboard), new { id = createInputRequest.SurveyId });

                /*string inputTag = HtmlHelpers.GenerateInputTag(inputType, model);

                ViewBag.InputTag = inputTag;

                return View(nameof(Designer));*/
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
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
                    return NotFound();
                }

                var inputDto = existingInput.Input;

                return View(inputDto);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
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
                    return View(nameof(EditInput), updatedInputRequest);
                }

                var response = await _inputRepository.UpdateInput(new UpdateInputRequest() { Input = updatedInputRequest });

                return RedirectToAction(nameof(SurveyDashboard), new { id = updatedInputRequest.SurveyId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        #endregion

        #region Delete Input

        public async Task<IActionResult> DeleteInput(DeleteInputRequest deleteInputRequest, int surveyId)
        {
            try
            {
                var response = await _inputRepository.DeleteInput(deleteInputRequest);

                return RedirectToAction(nameof(SurveyDashboard), new { id = surveyId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        #endregion

        #endregion

        #region FormDesign
        public async Task<IActionResult> PreviewSurvey(GetInputsBasedOnSurveyIdRequest request)
        {
            try
            {
                var response = await _inputRepository.GetInputsBySurveyId(request);

                if (response is null || response.Inputs.Count is 0)
                {
                    return BadRequest();
                }

                string allHtml = HtmlHelper.GenerateForm(response.Inputs);

                ViewBag.InputTag = allHtml;

                return View(nameof(SurveyOutput));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
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
                return RedirectToAction("Error", ex);
            }
        } 
        #endregion
    }
}
