using FormBuilderMVC.Models;
using FormBuilderMVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderMVC.Controllers
{
    public class FormBuilderController : Controller
    {
        private static List<Surveys> _surveys =
        [
            new Surveys()
            {
                Id = 1, Title="Employee form", OpenDate = DateTime.Now.Date, EndDate = DateTime.Now.Date , Inputs = new List<Inputs>
                {
                    new Inputs()
                    {
                        Id = 1, SurveyId= 1, InputType = "Text", Label ="First Name", InternalName = "Name TextBox"
                    },
                    new Inputs()
                    {
                        Id = 2, SurveyId= 1, InputType = "Number", Label ="Phone number", InternalName = "My Number"
                    }
                }
            },
            new Surveys()
            { Id = 2, Title="Delete form", OpenDate = DateTime.Now.Date, EndDate = DateTime.Now.Date },
            new Surveys()
            { Id = 3, Title="Feedback form", OpenDate = DateTime.Now.Date, EndDate = DateTime.Now.Date }
        ];

        public IActionResult SurveyDashboard()
        {
            try
            {
                return View(_surveys);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        public IActionResult CreateSurvey()
        {
            try
            {
                return View(new Surveys()
                {
                    Inputs = []
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSurvey(Surveys survey)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(nameof(CreateSurvey), survey);
                }

                int maxId = _surveys.Max(s => s.Id);

                survey.Id = maxId + 1;

                _surveys.Add(survey);

                return RedirectToAction(nameof(SurveyDashboard));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        public IActionResult EditSurvey(int id)
        {
            try
            {
                var survey = _surveys.FirstOrDefault(x => x.Id == id);

                if (survey == null)
                {
                    return NotFound();
                }

                return View(survey);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSurvey(int? id, Surveys updatedSurvey)
        {
            try
            {
                if (id != updatedSurvey.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return View(nameof(EditSurvey), updatedSurvey);
                }

                var existingSurvey = _surveys.FirstOrDefault(s => s.Id == id);

                if (existingSurvey == null)
                {
                    return NotFound();
                }

                existingSurvey.Title = updatedSurvey.Title;
                existingSurvey.OpenDate = updatedSurvey.OpenDate;
                existingSurvey.EndDate = updatedSurvey.EndDate;

                return RedirectToAction(nameof(SurveyDashboard));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        public IActionResult DeleteSurvey(int id)
        {
            try
            {
                var surveyToRemove = _surveys.FirstOrDefault(s => s.Id == id);
                if (surveyToRemove != null)
                {
                    _surveys.Remove(surveyToRemove);
                }
                else
                {
                    return RedirectToAction("Error");
                }

                return RedirectToAction(nameof(SurveyDashboard));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }


        public IActionResult CreateInput()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitInput(Inputs model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(nameof(CreateInput), model);
                }

                if (!Enum.TryParse(model.InputType, true, out HtmlInputType inputType))
                {
                    return RedirectToAction("Error");
                }

                var employeeFormSurvey = _surveys.FirstOrDefault(s => s.Id == model.SurveyId);
                if (employeeFormSurvey != null)
                {
                    int maxId = employeeFormSurvey.Inputs?.Max(i => i.Id) ?? 0;

                    model.Id = maxId + 1;

                    if (employeeFormSurvey.Inputs == null)
                    {
                        employeeFormSurvey.Inputs = new List<Inputs>();
                    }

                    employeeFormSurvey.Inputs.Add(model);
                }
                else
                {
                    return RedirectToAction("Error");
                }

                string inputTag = HtmlHelpers.GenerateInputTag(inputType, model);

                ViewBag.InputTag = inputTag;

                return View(nameof(Designer));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        public IActionResult EditInput(int inputId, int surveyId)
        {
            try
            {
                var existingSurvey = _surveys.FirstOrDefault(s => s.Id == surveyId);

                var existingInput = existingSurvey?.Inputs?.FirstOrDefault(x => x.Id == inputId);

                if (existingInput == null)
                {
                    return NotFound();
                }

                return View(existingInput);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateInput(int? id, Inputs updatedInput)
        {
            try
            {
                if (id != updatedInput.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return View(nameof(EditInput), updatedInput);
                }

                var existingSurvey = _surveys.FirstOrDefault(s => s.Id == updatedInput.SurveyId);

                var existingInput = existingSurvey?.Inputs?.FirstOrDefault(x => x.Id == id);

                if (existingInput == null)
                {
                    return NotFound();
                }

                existingInput.InputType = updatedInput.InputType;
                existingInput.InternalName = updatedInput.InternalName;
                existingInput.Label = updatedInput.Label;

                return RedirectToAction(nameof(SurveyDashboard));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        public IActionResult DeleteInput(int id)
        {
            try
            {
                var surveyToRemove = _surveys.FirstOrDefault(s => s.Id == id);
                if (surveyToRemove != null)
                {
                    _surveys.Remove(surveyToRemove);
                }
                else
                {
                    return RedirectToAction("Error");
                }

                return RedirectToAction(nameof(SurveyDashboard));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        public IActionResult Designer()
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
        public IActionResult PreviewSurvey(int id)
        {
            try
            {
                var survey = _surveys.FirstOrDefault(x => x.Id == id);

                if (survey is null && survey?.Inputs?.Count > 0)
                {
                    return BadRequest();
                }

                string allHtml = HtmlHelpers.GenerateForm(survey?.Inputs);

                ViewBag.InputTag = allHtml;

                return View(nameof(Designer));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }
    }
}
